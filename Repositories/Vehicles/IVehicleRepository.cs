namespace App.Repositories.Vehicles
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<Vehicle?> GetByNumberPlateAsync(string numberPlate);
        Task<List<Vehicle>> GetAllAsync();
        Task<List<Vehicle>> GetVehiclesByBrandAsync(string brand);
    }
}
