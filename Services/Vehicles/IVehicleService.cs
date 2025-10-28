using App.Repositories;
using App.Repositories.Vehicles;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles
{
    public interface IVehicleService
    {
        
        Task <ServiceResult<VehicleDto>> GetByIdAsync(int id);
        Task<ServiceResult<CreateVehicleResponse>> CreateAsync(CreateVehicleRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> UpdateAsync(int id, UpdateVehicleRequest request);
        Task<ServiceResult<List<VehicleDto>>> GetAllAsync();
        Task<ServiceResult<List<VehicleDto>>> GetPagedAllListAsync(int pagenumber, int pagesize);




    }
}
                                                                                                                           