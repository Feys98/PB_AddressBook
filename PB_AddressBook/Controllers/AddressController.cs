using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB_AddressBook.DataBaseContext;
using PB_AddressBook.Model;
using PB_AddressBook.Services;

namespace PB_AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBookService _service;

        public AddressController(IAddressBookService service)
        {
            _service = service;
        }


        // GET: api/Address/LastAddress
        [HttpGet("LastAddress")]
        public async Task<IActionResult> GetLastAddress()
        {
            AddressBook address;
            try
            {
                address = (await _service.GetAll()).Last();
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
                addresses = await _service.GetBy(x => x.City == city);
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

            try
            {
                await _service.Update(addressBook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> PostAddressBook(AddressBook addressBook)
        {
            try
            {
                await _service.Create(addressBook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction("GetLastAddress", new { id = addressBook.Id }, addressBook);
        }

        //DELETE: api/Address/<id>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressBook(int id)
        {
            try
            {
                var address = await _service.GetSingle(id);
                await _service.Delete(address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}