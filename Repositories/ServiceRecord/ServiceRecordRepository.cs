using App.Repositories.Service;
using App.Repositories.Services;
using App.Repositories.Vehicles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Repositories.Services
{
    public class ServiceRecordRepository(AppDbContext context) : GenericRepository<ServiceRecord>(context), IServiceRecordRepository
    {

        public async Task<List<ServiceRecord>> GetAllAsync()
        {
            return await Context.serviceRecords.AsNoTracking().ToListAsync();
        }

        public void Update(ServiceRecord serviceRecord)
        {
            Context.serviceRecords.Update(serviceRecord);
        }
        public void Delete(ServiceRecord serviceRecord)
        {
            Context.serviceRecords.Remove(serviceRecord);
        }
     


        public async Task<List<ServiceRecord>> GetByVehicleIdAsync(int vehicleId)
        {
            return await Context.serviceRecords
                .AsNoTracking()
                .Where(record => record.VehicleId == vehicleId)
                .ToListAsync();
        }
       

    }
}
