using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wedding_Planner.Models;

namespace Wedding_Planner.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Privacy()
		{
			return View();
		}


        [HttpGet]
        public IActionResult PlannerDashboard()
        {
            // Example dummy data — you can replace this with real data from _context
            ViewBag.ActiveWeddings = 5;
            ViewBag.UpcomingTasks = 3;
            ViewBag.TotalBudget = 15000;
            ViewBag.VendorContracts = 4;

            ViewBag.MyWeddings = new List<dynamic>
        {
            new { CoupleName = "John & Jane", Date = DateTime.Today, Venue = "Palm Garden", Status = "Active" },
            new { CoupleName = "Mark & Lisa", Date = DateTime.Today.AddDays(30), Venue = "Ocean View", Status = "Pending" }
        };

            ViewBag.UpcomingTasksList = new List<dynamic>
        {
            new { Name = "Cake Tasting", WeddingName = "John & Jane", DueDate = DateTime.Today.AddDays(2), Status = "Pending" },
            new { Name = "Rehearsal", WeddingName = "Mark & Lisa", DueDate = DateTime.Today.AddDays(5), Status = "Completed" }
        };

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
