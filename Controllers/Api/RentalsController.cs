using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.Entity;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RentalsController(IMapper mapper)
        {
            _context = new ApplicationDbContext();
            _mapper = mapper;
        }
        // POST api/rentals/
        [HttpPost]
        public async Task<IActionResult> CreateRentals(RentalDTO rentalDTO)
        {
            var customer = await _context.Customers.SingleAsync(
                c => c.Id == rentalDTO.CustomerId);

            var movies = _context.Movies.Where(
                m => rentalDTO.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest($"Movie: {movie.Name} is not available");
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    RentalDate = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
