using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB_AddressBook.DataBaseContext;
using PB_AddressBook.Model;

namespace PB_AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AddressController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Address/LastAddress
        [HttpGet("LastAddress")]
        public async Task<ActionResult<IEnumerable<AddressBook>>> GetLastAddress()
        {
            AddressBook address;
            try
            {
                address = (await _context.Address.ToListAsync()).Last();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(address);
        }

        // GET: api/Address/'<City>'
        [HttpGet("{city}")]
        public async Task<IActionResult> GetAddressBook(string city)
        {
            List<AddressBook> addresses;
            try
            {
                addresses = await _context.Address.Where(x => x.City == city).ToListAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(addresses);
        }

        // PUT: api/Address/<id>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressBook(int id, AddressBook addressBook)
        {
            if (id != addressBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressBookExists(id))
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

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> PostAddressBook(AddressBook addressBook)
        {
            try
            {
                _context.Address.Add(addressBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction("GetLastAddress", new { id = addressBook.Id }, addressBook);
        }

        // DELETE: api/Address/<id>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressBook(int id)
        {
            var addressBook = await _context.Address.FindAsync(id);
            if (addressBook == null)
            {
                return NotFound();
            }

            _context.Address.Remove(addressBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressBookExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}