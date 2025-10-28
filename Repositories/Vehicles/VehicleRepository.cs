using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Vehicles
{
    public class VehicleRepository(AppDbContext context) : GenericRepository<Vehicle>(context), IVehicleRepository
    {
        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await Context.Vehicles.AsNoTracking().ToListAsync();
        }

        public  Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand)
        {
            return  Context.Vehicles
                .Where(v => v.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public void Update(Vehicle vehicle)
        {
                  
            Context.Vehicles.Update(vehicle);
         }
        public void Delete(Vehicle vehicle)
        {
            Context.Vehicles.Remove(vehicle);
        }

        public async Task<Vehicle> GetByNumberPlateAsync(string numberPlate)
        {
            return await Context.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.NumberPlate == numberPlate);
        }
    }
}
