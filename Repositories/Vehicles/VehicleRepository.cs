using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Vehicles
{
    public class VehicleRepository(AppDbContext context) : GenericRepository<Vehicle>(context), IVehicleRepository
    {
        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await Context.Vehicles.AsNoTracking().ToListAsync();
        }

        public Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand)
        {
            return Context.Vehicles
                .Where(v => v.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByNumberPlateAsync(string numberPlate)
        {
            return await Context.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.NumberPlate == numberPlate);
        }
    }
}
