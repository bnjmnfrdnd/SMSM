using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interface.Models;
using Interface.Data;
using Microsoft.EntityFrameworkCore;

namespace Interface.Controllers
{
    public class HomeController : Controller
    {
        #region Properties

        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext database;

        #endregion

        #region Logger

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            database = context;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Requests()
        {
            return View();
        }

        public IActionResult Movies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Movies

        [HttpGet]
        public ActionResult GetMovies()
        {
            try
            {
                List<Movie> movies = database.Movies.AsNoTracking().ToList();

                return Json(movies);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetMovie(int movieId)
        {
            try
            {
                Movie movie = database.Movies.AsNoTracking().SingleOrDefault(i => i.ID == movieId);

                return Json(movie);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveMovie(Movie movie)
        {
            try
            {
                List<Movie> movies = database.Movies.AsNoTracking().ToList();

                if (movie.Title == null)
                {
                    return Json("A title is required");
                }

                if (movie.YYYY == null)
                {
                    return Json("A year is required");
                }

                foreach (Movie m in movies)
                {
                    if (movie.Title == m.Title && movie.YYYY == m.YYYY)
                    {
                        return Json("A movie with this title and year already exists");
                    }
                }

                if (movie.ID == 0) database.Entry(movie).State = EntityState.Added;

                else database.Entry(movie).State = EntityState.Modified;

                database.SaveChanges();

                return View("Movies");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion

        #region Requests

        [HttpGet]
        public JsonResult GetRequests()
        {
            try
            {
                

                List<Request> requests = database.Requests.AsNoTracking().Where(i => i.IsComplete == false).ToList();

               

                    return Json(requests);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SaveRequest(Request request)
        {
            try
            {
                List<Request> requests = database.Requests.AsNoTracking().ToList();

                Movie movie = new Movie();
                TV tvShow = new TV();

                if (request != null)
                {
                    if (request.IsComplete)
                    {
                        switch (request.Type)
                        {
                            case "Movie":
                                movie.Title = request.Title;
                                movie.YYYY = request.Year;
                                database.Entry(movie).State = EntityState.Added;
                                break;
                        }

                        switch (request.Type)
                        {
                            case "TV Show":
                                tvShow.Title = request.Title;
                                tvShow.Year = request.Year;
                                database.Entry(tvShow).State = EntityState.Added;
                                break;
                        }
                    }

                    if (request.Title == null)
                    {
                        return Json("A title is required");
                    }

                    if (request.Year == null)
                    {
                        return Json("A year is required");
                    }

                    if (request.RequestedBy == null)
                    {
                        return Json("A name is required");
                    }

                    request.RequestedDate = DateTime.Now;

                    foreach (Request r in requests)
                    {
                        if (request.Title == r.Title && request.Year == r.Year && r.ID != request.ID)
                        {
                            return Json("This request has already been made by " + r.RequestedBy + " on " + r.RequestedDate.ToString("dd/MM/yyyy"));
                        }
                    }

                    if (request.ID == 0) database.Entry(request).State = EntityState.Added;

                    else
                    {
                        database.Entry(request).State = EntityState.Modified;
                    }

                    database.SaveChanges();
                }

                return View("Requests");

            }
             catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetRequest(int requestId)
        {
            try
            {
                Request request = database.Requests.AsNoTracking().SingleOrDefault(i => i.ID == requestId);

                return Json(request);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteRequest(int id)
        {
            try
            {
                Request request = database.Requests.AsNoTracking().SingleOrDefault(i => i.ID == id);

                database.Entry(request).State = EntityState.Deleted;

                database.SaveChanges();

                return Json("Request deleted!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion
    }
}
