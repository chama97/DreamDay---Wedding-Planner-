using Wedding_Planner.Models.DTO;
using Wedding_Planner.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Services.Impl
{
    public class VendorsService : IVendorService
    {
        private readonly WeddingPlannerDbContext _context;

        public VendorsService(WeddingPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VendorViewModel>> GetAllVendorsAsync()
        {
            var vendors = await _context.Vendors
                .Include(v => v.Category)
                .ToListAsync();

            return vendors.Select(v => new VendorViewModel
            {
                Id = v.Id,
                VendorName = v.VendorName,
                CategoryId = v.CategoryId,
                ContactPerson = v.ContactPerson,
                Email = v.Email,
                Phone = v.Phone,
                Website = v.Website,
                Address = v.Address,
                Description = v.Description,
                HourlyRate = v.HourlyRate,
                IsActive = v.IsActive
            });
        }

        public async Task<VendorViewModel> GetVendorByIdAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null) return null;

            return new VendorViewModel
            {
                Id = vendor.Id,
                VendorName = vendor.VendorName,
                CategoryId = vendor.CategoryId,
                ContactPerson = vendor.ContactPerson,
                Email = vendor.Email,
                Phone = vendor.Phone,
                Website = vendor.Website,
                Address = vendor.Address,
                Description = vendor.Description,
                HourlyRate = vendor.HourlyRate,
                IsActive = vendor.IsActive
            };
        }

        public async Task CreateVendorAsync(VendorViewModel model)
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
                IsActive = model.IsActive
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVendorAsync(VendorViewModel model)
        {
            var vendor = await _context.Vendors.FindAsync(model.Id);
            if (vendor == null) return;

            vendor.VendorName = model.VendorName;
            vendor.CategoryId = model.CategoryId;
            vendor.ContactPerson = model.ContactPerson;
            vendor.Email = model.Email;
            vendor.Phone = model.Phone;
            vendor.Website = model.Website;
            vendor.Address = model.Address;
            vendor.Description = model.Description;
            vendor.HourlyRate = model.HourlyRate;
            vendor.IsActive = model.IsActive;

            _context.Vendors.Update(vendor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVendorAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
