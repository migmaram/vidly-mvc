using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{    
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageCustomers))
                return View("List");
            return View("ReadOnlyList");
        }
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public IActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public IActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var existingCustomer = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);

                existingCustomer.Name = customer.Name;
                existingCustomer.Birthdate = customer.Birthdate;
                existingCustomer.MembershipTypeId = customer.MembershipTypeId;
                existingCustomer.IsSuscribedToNewsletter = customer.IsSuscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}
