using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wedding_Planner.Models.DTO;
using Wedding_Planner.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Controllers
{
    public class VenderController : Controller
    {
        private readonly WeddingPlannerDbContext _context;

        public VenderController(WeddingPlannerDbContext context)
        {
            _context = context;
        }

        // View all vendors for couple
        public IActionResult Get()
        {
            // Check if user is logged in and is a couple
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || role != "Couple")
            {
                return RedirectToAction("Login", "Account");
            }

            return View("ManageVendors");
        }

        // View all vendors for admin
        public async Task<IActionResult> ManageVenders()
        {
            var vendors = await _context.Vendors
                .Include(v => v.Category)
                .ToListAsync();

            return View(vendors);
        }

        // GET: Create Vendor
        public IActionResult CreateVender()
        {
            var model = new VendorViewModel
            {
                Categories = _context.VendorCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
            };

            return View(model);
        }

        // POST: Create Vendor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVender(VendorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vendor = new Vendor
                {
                    VendorName = model.VendorName,
                    CategoryId = model.CategoryId,
                    ContactPerson = model.ContactPerson,
                    Email = model.Email,
                    Phone = model.Phone,
                    Website = model.Website,
                    Address = model.Address,
                    Description = model.Description,
                    HourlyRate = model.HourlyRate,
                    IsActive = true
                };

                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageVenders));
            }

            // Reload dropdown if model invalid
            model.Categories = _context.VendorCategories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(model);
        }
    }
}
