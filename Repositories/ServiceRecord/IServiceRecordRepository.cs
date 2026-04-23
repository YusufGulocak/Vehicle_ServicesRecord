using App.Repositories.Service;

namespace App.Repositories.Services
{
    public interface IServiceRecordRepository : IGenericRepository<ServiceRecord>
    {
        Task<List<ServiceRecord>> GetAllAsync();
        Task<List<ServiceRecord>> GetByVehicleIdAsync(int vehicleId);
    }
}
