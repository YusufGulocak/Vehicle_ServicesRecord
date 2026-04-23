using App.Repositories.Service;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Services
{
    public class ServiceRecordRepository(AppDbContext context) : GenericRepository<ServiceRecord>(context), IServiceRecordRepository
    {
        public async Task<List<ServiceRecord>> GetAllAsync()
        {
            return await Context.ServiceRecords.AsNoTracking().ToListAsync();
        }

        public async Task<List<ServiceRecord>> GetByVehicleIdAsync(int vehicleId)
        {
            return await Context.ServiceRecords
                .AsNoTracking()
                .Where(record => record.VehicleId == vehicleId)
                .ToListAsync();
        }
    }
}
