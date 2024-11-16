using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CustomersController(IMapper mapper)
        {
            _context = new ApplicationDbContext();
            _mapper = mapper;
        }
        // GET: api/customers/
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers.
                Include(c => c.MembershipType).
                ToListAsync();
            return Ok(_mapper.Map<List<CustomerDTO>>(customers));
        }

        // GET: /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }
        // POST: /api/customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<Customer>(customerDTO);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDTO.Id = customer.Id;

            return Created(new Uri(Request.GetEncodedUrl() + "/" + customerDTO.Id), new { id = customerDTO.Id, customerDTO });
        }

        // PUT: /api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _mapper.Map(customerDTO, customerInDb);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: /api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
