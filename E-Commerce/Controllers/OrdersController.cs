using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Models.Dtos;
using E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ECommerceContext _dbContext;
        private readonly OrderReadContext _dbReadContext;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(ECommerceContext dbContext, OrderReadContext dbReadContext, ILogger<OrdersController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbReadContext = dbReadContext;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(int pageNumber = 1, int pageSize = 10)
        {
            var orders = await _dbReadContext.Orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }

            try
            {
                var order = _mapper.Map<Order>(orderDto);
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, _mapper.Map<OrderDto>(order));
            }
            catch (DbUpdateException ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while adding the order.");

                // Return a 500 error.
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An unexpected error occurred.");

                // Return a 500 error.
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Orders/1
           [HttpGet("{id}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _dbReadContext.Orders.FindAsync(id);

            if (order == null)
            {
                System.Diagnostics.Debug.WriteLine($"Order with id {id} not found");
                return NotFound();
            }

            var mappedOrder = _mapper.Map<OrderDto>(order);        

            if (mappedOrder == null)
            {
                System.Diagnostics.Debug.WriteLine("Mapper returned null");
                // TODO: handle this error
            }

            return mappedOrder;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderDto, order);
            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _dbContext.Orders.Any(e => e.Id == id);
        }
    }
}

