using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<CustomerDTO>>>GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        // GET: /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        { 
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return _mapper.Map<CustomerDTO>(customer);
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

            customerDTO.Id = customerDTO.Id;

            return CreatedAtAction(nameof(Customer), new { id = customerDTO.Id, customerDTO});
        }

        // PUT: /api/customers/{id}
        [HttpPut]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _mapper.Map(customerInDb, customerDTO);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: /api/customers/{id}
        [HttpDelete]
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
