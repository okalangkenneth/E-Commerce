using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Models;
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
    public class ProductImagesController : ControllerBase
    {
        private readonly ECommerceContext _dbContext;
        private readonly ILogger<ProductImagesController> _logger;
        private readonly IMapper _mapper;

        public ProductImagesController(ECommerceContext dbContext, ILogger<ProductImagesController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/ProductImages
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ProductImageDto>>> GetProductImages(int pageNumber = 1, int pageSize = 10)
        {
            var productImages = await _dbContext.ProductImages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<ProductImageDto>>(productImages);
        }

        // POST: api/ProductImages
        [HttpPost]
        public async Task<ActionResult<ProductImageDto>> PostProductImage(ProductImageDto productImageDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }

            try
            {
                var productImage = _mapper.Map<ProductImage>(productImageDto);
                _dbContext.ProductImages.Add(productImage);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProductImage), new { id = productImage.Id }, _mapper.Map<ProductImageDto>(productImage));
            }
            catch (DbUpdateException ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while adding the product image.");

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

        // GET: api/ProductImages/1
        [HttpGet("{id}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult<ProductImageDto>> GetProductImage(int id)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductImageDto>(productImage);
        }

        // PUT: api/ProductImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage(int id, ProductImageDto productImageDto)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            _mapper.Map(productImageDto, productImage);
            _dbContext.Entry(productImage).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
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

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            _dbContext.ProductImages.Remove(productImage);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductImageExists(int id)
        {
            return _dbContext.ProductImages.Any(e => e.Id == id);
        }
    }
}

