using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Models.Dtos;
using E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Provides API endpoints for managing Customers
    /// </summary>
    public class CustomersController : ControllerBase
    {
        private readonly ECommerceContext _dbContext;
        private readonly CustomerReadContext _dbReadContext;
        private readonly ILogger<CustomersController> _logger;
        private readonly IMapper _mapper;

        public CustomersController(ECommerceContext dbContext, CustomerReadContext dbReadContext, ILogger<CustomersController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbReadContext = dbReadContext;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of Customers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Customers
        /// </remarks>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>List of Customers</returns>
        /// <response code="200">Returns the list of Customers</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="500">If there is a server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers(int pageNumber = 1, int pageSize = 10)
        {
            var customers = await _dbReadContext.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<CustomerDto>>(customers);
        }

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Customers
        /// </remarks>
        /// <param name="customerDto">The Customer data transfer object</param>
        /// <returns>A newly created Customer</returns>
        /// <response code="201">Returns the newly created Customer</response>
        /// <response code="400">If the input is invalid</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="500">If there is a server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        }

        // GET: api/Customers/5
        /// <summary>
        /// Retrieve a specific customer by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Customer/id
        /// </remarks>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <response code="200">Returns the specified customer.</response>
        /// <response code="404">If no customer is found with the provided ID.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _dbReadContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        // PUT: api/Customers/5
        /// <summary>
        /// Update a specific customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     UPDATE /api/Customer/id
        /// </remarks>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerDto">The updated data for the customer.</param>
        /// <response code="204">If the customer is updated successfully.</response>
        /// <response code="400">If the provided data is invalid.</response>
        /// <response code="404">If no customer is found with the provided ID.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _mapper.Map(customerDto, customer);
            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // DELETE: api/Customers/5
        /// <summary>
        /// Delete a specific customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Customer/id
        /// </remarks>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <response code="204">If the customer is deleted successfully.</response>
        /// <response code="404">If no customer is found with the provided ID.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _dbContext.Customers.Any(e => e.Id == id);
        }
    }
}

