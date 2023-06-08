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
    public class AddressesController : ControllerBase
    {
        private readonly ECommerceContext _dbContext;
        private readonly ILogger<AddressesController> _logger;
        private readonly IMapper _mapper;

        public AddressesController(ECommerceContext dbContext, ILogger<AddressesController> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Addresses
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses(int pageNumber = 1, int pageSize = 10)
        {
            var addresses = await _dbContext.Addresses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<AddressDto>>(addresses);
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<AddressDto>> PostAddress(AddressDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }

            try
            {
                var address = _mapper.Map<Address>(addressDto);
                _dbContext.Addresses.Add(address);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, _mapper.Map<AddressDto>(address));
            }
            catch (DbUpdateException ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while adding the address.");

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

        // GET: api/Addresses/1
        [HttpGet("{id}")]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                System.Diagnostics.Debug.WriteLine($"Address with id {id} not found");
                return NotFound();
            }

            var mappedAddress = _mapper.Map<AddressDto>(address);

            if (mappedAddress == null)
            {
                System.Diagnostics.Debug.WriteLine("Mapper returned null");
                // TODO: handle this error
            }

            return mappedAddress;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, AddressDto addressDto)
        {
            var address = await _dbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _mapper.Map(addressDto, address);
            _dbContext.Entry(address).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return _dbContext.Addresses.Any(e => e.Id == id);
        }
    }
}
