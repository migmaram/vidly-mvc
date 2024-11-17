using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Text.Encodings.Web;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET: /Movies/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult New()
        {
            var viewModel = new MovieViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var existingMovie = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);

                existingMovie.Name = movie.Name;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.DateAdded = DateTime.Now;
                existingMovie.GenreId = movie.GenreId;
                existingMovie.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}

