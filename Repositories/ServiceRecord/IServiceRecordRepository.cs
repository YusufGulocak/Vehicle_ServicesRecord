using App.Repositories.Service;
using App.Repositories.Services;
using App.Repositories.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Services
{
        public interface IServiceRecordRepository:IGenericRepository<ServiceRecord>

        {
            Task<List<ServiceRecord>> GetAllAsync();
            void Update(ServiceRecord serviceRecord);
            void Delete(ServiceRecord serviceRecord);
            Task<List<ServiceRecord>>GetByVehicleIdAsync(int vehicleId);
        }
}
