using Wedding_Planner.Models.DTO;

namespace Wedding_Planner.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorViewModel>> GetAllVendorsAsync();
        Task<VendorViewModel> GetVendorByIdAsync(int id);
        Task CreateVendorAsync(VendorViewModel model);
        Task UpdateVendorAsync(VendorViewModel model);
        Task DeleteVendorAsync(int id);

    }
}
