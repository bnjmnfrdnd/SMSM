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

        List<RequestType> requestTypes = new List<RequestType>();

        List<RequestUser> requestUsers = new List<RequestUser>();

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
            return View("Home.Index");
        }

        public IActionResult Privacy()
        {
            return View("Home.Privacy");
        }

        public IActionResult Requests()
        {
            requestTypes = database.RequestTypes.AsNoTracking().Where(i => i.Enabled == true).ToList();
            
            requestUsers = database.RequestUsers.AsNoTracking().Where(i => i.Active == true).ToList();

            ViewBag.requestTypes = requestTypes;

            ViewBag.requestUsers = requestUsers;

            return View("Request.Home");
        }

        public IActionResult Movies()
        {
            return View("Media.Movies");
        }

        public IActionResult TVShows()
        {
            return View("Media.TVShows");
        }

        public IActionResult RequestUsers()
        {
            return View("Settings.RequestUsers");
        }

        public IActionResult RequestTypes()
        {
            return View("Settings.RequestTypes");
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

                return View("Media.Movies");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteMovie(int id)
        {
            try
            {
                Movie movie = database.Movies.AsNoTracking().SingleOrDefault(i => i.ID == id);

                database.Entry(movie).State = EntityState.Deleted;

                database.SaveChanges();

                return Json("Movie deleted!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        #endregion

        #region TV Shows

        [HttpGet]
        public ActionResult GetTVShows()
        {
            try
            {
                List<TVShows> tvshows = database.TVShows.AsNoTracking().ToList();

                return Json(tvshows);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetTVShow(int tvShowId)
        {
            try
            {
                TVShows tvshow = database.TVShows.AsNoTracking().SingleOrDefault(i => i.ID == tvShowId);

                return Json(tvshow);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveTVShow(TVShows tvshow)
        {
            try
            {
                List<TVShows> tvshows = database.TVShows.AsNoTracking().ToList();

                if (tvshow != null)
                {

                    if (tvshow.Title == null)
                    {
                        return Json("A title is required");
                    }

                    if (tvshow.Year == null)
                    {
                        return Json("A year is required");
                    }

                    foreach (TVShows tv in tvshows)
                    {
                        if (tvshow.Title == tv.Title && tvshow.Year == tv.Year)
                        {
                            return Json("A TV show with this title and year already exists");
                        }
                    }

                    if (tvshow.ID == 0) database.Entry(tvshow).State = EntityState.Added;

                    else database.Entry(tvshow).State = EntityState.Modified;
                }

                database.SaveChanges();

                return View("Media.TVShows");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteTVShow(int id)
        {
            try
            {
                TVShows tvShow = database.TVShows.AsNoTracking().SingleOrDefault(i => i.ID == id);

                database.Entry(tvShow).State = EntityState.Deleted;

                database.SaveChanges();

                return Json("TV Show Deleted!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion

        #region Requests

        [HttpPost]
        public IActionResult SortMovieSearch(JsonResult jsonResult)
        {
            try
            {
                Movie newMovie = new Movie();
                List<Movie> movies = new List<Movie>();

                return Json("");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetRequests()
        {
            try
            {
                List<Request> requests = database.Requests.AsNoTracking().ToList();
                
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

                TVShows tvShow = new TVShows();

                requestTypes = database.RequestTypes.AsNoTracking().Where(i => i.Enabled == true).ToList();

                requestUsers = database.RequestUsers.AsNoTracking().Where(i => i.Active == true).ToList();

                ViewBag.requestTypes = requestTypes;

                ViewBag.requestUsers = requestUsers;

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

                return View("Request.Home");

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

        #region Request Users

        [HttpGet]
        public JsonResult GetRequestUsers()
        {
            try
            {
                List<RequestUser> requestUsers = database.RequestUsers.AsNoTracking().OrderBy(x => x.Name).ToList();

                return Json(requestUsers);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveRequestUser(RequestUser requestUser)
        {
            try
            {
                List<RequestUser> requestUsers = database.RequestUsers.AsNoTracking().ToList();

                if (requestUser.Name == null)
                {
                    return Json("A name is required");
                }

                foreach (RequestUser rU in requestUsers)
                {
                    if (requestUser.Name == rU.Name && requestUser.ID != rU.ID)
                    {
                        return Json("A type with this name already exists");
                    }
                }

                if (requestUser.ID == 0) database.Entry(requestUser).State = EntityState.Added;

                else database.Entry(requestUser).State = EntityState.Modified;

                database.SaveChanges();

                return View("Settings.RequestUsers");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetRequestUser(int requestUserId)
        {
            try
            {
                RequestUser requestUser = database.RequestUsers.AsNoTracking().SingleOrDefault(i => i.ID == requestUserId);

                return Json(requestUser);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion

        #region Request Types

        public JsonResult GetRequestTypes()
        {
            try
            {
                List<RequestType> requestTypes = database.RequestTypes.AsNoTracking().ToList();

                return Json(requestTypes);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveRequestType(RequestType requestType)
        {
            try
            {
                List<RequestType> requestTypes = database.RequestTypes.AsNoTracking().ToList();

                if (requestType.Type == null)
                {
                    return Json("A type name is required");
                }

                foreach (RequestType rT in requestTypes)
                {
                    if (requestType.Type == rT.Type && requestType.ID != rT.ID)
                    {
                        return Json("A type with this name already exists");
                    }
                }

                if (requestType.ID == 0) database.Entry(requestType).State = EntityState.Added;

                else database.Entry(requestType).State = EntityState.Modified;

                database.SaveChanges();

                return View("Settings.RequestTypes");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteRequestType(int requestTypeId)
        {
            try
            {
                RequestType requestType = database.RequestTypes.AsNoTracking().SingleOrDefault(i => i.ID == requestTypeId);

                database.Entry(requestType).State = EntityState.Deleted;

                database.SaveChanges();

                return Json("Request type deleted!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetRequestType(int requestTypeId)
        {
            try
            {
                RequestType requestType = database.RequestTypes.AsNoTracking().SingleOrDefault(i => i.ID == requestTypeId);

                return Json(requestType);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion

        #region RSS

        #endregion
    }
}