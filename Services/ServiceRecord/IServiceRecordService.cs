using App.Repositories.Service;
using App.Services.Servicess;
using App.Services.Servicess.Create;
using App.Services.Servicess.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services
{
   public interface  IServiceRecordService
    {
        Task<ServiceResult<ServiceRecordsDto>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<ServiceRecordsDto>>> GetAllAsync();
        Task<ServiceResult<IEnumerable<ServiceRecordsDto>>> GetByVehicleIdAsync(int vehicleId);
        Task<ServiceResult<CreateServiceRecordResponse>> CreateAsync(CreateServiceRecordRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateServiceRecordRequest request);
        Task<ServiceResult> DeleteAsync(int id);   
        
    }

}
