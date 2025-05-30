using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding_Planner.Models.DTO;
using Wedding_Planner.Models.Entity;
using Wedding_Planner.Services;

namespace Wedding_Planner.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly WeddingPlannerDbContext _context;

        public AccountController(IUserService userService, WeddingPlannerDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        //GET: Register View
        [HttpGet]
        public IActionResult RegisterCouple()
        {
            return View();
        }

        // GET: Register View
        [HttpGet]
        public IActionResult RegisterPlanner()
        {
            return View();
        }

        // GET: Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET: Logout View
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // POST: Register Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCouple(CoupleRegisterModel newCouple)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isRegistered = await _userService.RegisterCoupleAsync(newCouple);
                    if (isRegistered)
                    {
                        TempData["SuccessMessage"] = "Account Created Successfully!";
                        return RedirectToAction("Login");
                    }
                }
                return View(newCouple);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while registering couple request. Please try again later: {ex.Message}";
                return View("Error");
            }
        }


        // POST: Register Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPlanner(WeddingPlannerRegisterModel newPlanner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isRegistered = await _userService.RegisterWeddingPlannerAsync(newPlanner);
                    if (isRegistered)
                    {
                        TempData["SuccessMessage"] = "Account Created Successfully!";
                        return RedirectToAction("Login");
                    }
                }
                return View(newPlanner);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while creating planner account. Please try again later: {ex.Message}";
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var (isSuccess, user, errorMessage) = await _userService.LoginAsync(email, password);

                if (isSuccess && user != null)
                {
                    // Get Role name
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
                    string roleName = role?.Name ?? "Unknown";

                    // Store session data
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("UserID", user.Id.ToString());
                    HttpContext.Session.SetString("Role", roleName);
                    Console.WriteLine(user.Username);
                    Console.WriteLine(user.Id.ToString());
                    Console.WriteLine(roleName);

                    return roleName switch
                    {
                        "Admin" => RedirectToAction("AdminDashboard", "Home"),
                        "Planner" => RedirectToAction("PlannerDashboard", "Home"),
                        "Couple" => RedirectToAction("CoupleDashboard", "Home"),
                        _ => RedirectToAction("Index", "Home")
                    };
                }
                ViewBag.Error = errorMessage;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred during login. Please try again later. Error: {ex.Message}";
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var user = await _userService.GetUserByIdAsync(id.Value);

                if (user == null)
                {
                    return NotFound();
                }
                return Json(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while retrieving the user. Please try again later: {ex.Message}";
                return View("Error");
            }
        }


        // POST: User Delete By ID
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _userService.DeleteUserAsync(id);
                return Json(new { success });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the user. Please try again later: {ex.Message}";
                return View("Error");
            }
        }


        // POST : Change Password Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserID");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var success = await _userService.ChangePasswordAsync(userId, currentPassword, newPassword);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Current password is incorrect.";
                    return RedirectToAction("Setting");
                }

                TempData["SuccessMessage"] = "Password updated successfully!";
                return RedirectToAction("Setting");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while changing the user password. Please try again later: {ex.Message}";
                return View("Error");
            }
        }


    }
}
