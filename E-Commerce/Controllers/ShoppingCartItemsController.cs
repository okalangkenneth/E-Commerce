using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly ECommerceContext _dbContext;
        private readonly ShoppingCartItemReadContext _dbReadContext;
        private readonly ILogger<ShoppingCartItemsController> _logger;
        private readonly IMapper _mapper;

        public ShoppingCartItemsController(ECommerceContext dbContext, ShoppingCartItemReadContext dbReadContext, ILogger<ShoppingCartItemsController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbReadContext = dbReadContext;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/ShoppingCartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetShoppingCartItems(int pageNumber = 1, int pageSize = 10)
        {
            var shoppingCartItems = await _dbReadContext.ShoppingCartItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<ShoppingCartItemDto>>(shoppingCartItems);
        }

        // GET: api/ShoppingCartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartItemDto>> GetShoppingCartItem(int id)
        {
            var shoppingCartItem = await _dbReadContext.ShoppingCartItems.FindAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return _mapper.Map<ShoppingCartItemDto>(shoppingCartItem);
        }

        // POST: api/ShoppingCartItems
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItemDto>> PostShoppingCartItem(ShoppingCartItemDto shoppingCartItemDto)
        {
            var shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemDto);
            _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoppingCartItem), new { id = shoppingCartItem.Id }, _mapper.Map<ShoppingCartItemDto>(shoppingCartItem));
        }

        // PUT: api/ShoppingCartItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartItem(int id, ShoppingCartItemDto shoppingCartItemDto)
        {
            var shoppingCartItem = await _dbContext.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            _mapper.Map(shoppingCartItemDto, shoppingCartItem);
            _dbContext.Entry(shoppingCartItem).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemExists(id))
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

        // DELETE: api/ShoppingCartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartItem(int id)
        {
            var shoppingCartItem = await _dbContext.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartItemExists(int id)
        {
            return _dbContext.ShoppingCartItems.Any(e => e.Id == id);
        }
    }
}
