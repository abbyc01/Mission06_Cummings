using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6.Models;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context someName)
        {
            _context = someName;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Joel()
        {
            return View();
        }

        // ✅ Fix: Pass categories & ratings when loading the form
        [HttpGet]
        public IActionResult Movies()
        {
            ViewBag.Categories = _context.Categories.ToList(); // ✅ Pass category list to view
            ViewBag.Ratings = _context.Movies.Select(m => m.Rating).Distinct().ToList(); // ✅ Pass distinct ratings
            return View(new Movie()); // ✅ Ensure form is empty for adding a new movie
        }

        [HttpPost]
        public IActionResult Movies(Movie response)
        {
            if (response.MovieId == 0)  // ✅ If no ID, add new movie
            {
                _context.Movies.Add(response);
            }
            else  // ✅ If ID exists, update the existing record
            {
                _context.Movies.Update(response);
            }

            _context.SaveChanges();
            return RedirectToAction("MovieCollection", new Movie()); // ✅ Redirect instead of reloading form
        }

        public IActionResult MovieCollection()
        {
            var movies = _context.Movies.Include(m => m.Category).ToList(); // ✅ Fix incorrect `Categories` reference
            return View(movies);
        }

        // ✅ Fix: Use correct category reference and pass ViewBag.Categories & ViewBag.Ratings
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editMovie = _context.Movies.Include(m => m.Category)
                .FirstOrDefault(x => x.MovieId == id);

            if (editMovie == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList(); // ✅ Pass categories for dropdown
            ViewBag.Ratings = _context.Movies.Select(m => m.Rating).Distinct().ToList(); // ✅ Pass distinct ratings
            return View("Movies", editMovie); // ✅ Use the same form for adding/editing
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteMovie = _context.Movies
                .Single(x => x.MovieId == id);
            
            return View(deleteMovie);
        }

        [HttpPost]
        public IActionResult DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            
            return RedirectToAction("MovieCollection");
        }
    }
}
