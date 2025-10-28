using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Vehicles
{
    public interface IVehicleRepository: IGenericRepository<Vehicle>
    {
        Task<Vehicle> GetByNumberPlateAsync(string numberPlate);
        Task<List<Vehicle>> GetAllAsync();
        public Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand);

        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);   


    } 
}
