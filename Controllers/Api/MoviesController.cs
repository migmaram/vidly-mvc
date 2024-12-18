﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.Entity;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MoviesController(IMapper mapper)
        {
            _context = new ApplicationDbContext();
            _mapper = mapper;
        }
        // GET api/movies
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies() 
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();
            return Ok(_mapper.Map<List<MovieDTO>>(movies));
        }
        // GET api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovies(int id)
        {
            var movie = await _context.Movies                
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(_mapper.Map<MovieDTO>(movie));
        }
        // POST api/movies/
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public async Task<IActionResult> PostMovies(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<Movie>(movieDTO);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return Created(new Uri($"{Request.GetEncodedUrl()}/{movie.Id}"), new { id = movie.Id, movie });
        }
        // PUT api/movies/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public async Task<IActionResult> PutMovies(int id, MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            _mapper.Map(movieDTO, movieInDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // PUT api/movies/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleName.CanManageCustomers)]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var movieInDb = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();
            
            _context.Movies.Remove(movieInDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
