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
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }     
        public async Task<ActionResult<IEnumerable<Customer>>>GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        // GET: /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        { 
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return customer;
        }
        // POST: /api/customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customer.Id = customer.Id;

            return CreatedAtAction(nameof(Customer), new { id = customer.Id, customer});
        }

        // PUT: /api/customers/{id}
        [HttpPut]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSuscribedToNewsletter = customer.IsSuscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

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
