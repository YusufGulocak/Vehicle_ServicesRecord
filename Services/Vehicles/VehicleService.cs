using App.Repositories;
using App.Repositories.Vehicles;
using App.Services.ExceptionHandler;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Vehicles
{
    public class VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper) : IVehicleService
    {
        public async Task<ServiceResult<VehicleDto>> GetByIdAsync(int id)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
                return ServiceResult<VehicleDto>.Fail($"Vehicle with id {id} not found", HttpStatusCode.NotFound);

            return ServiceResult<VehicleDto>.Success(mapper.Map<VehicleDto>(vehicle));
        }

        public async Task<ServiceResult<List<VehicleDto>>> GetAllAsync()
        {
            var vehicles = await vehicleRepository.GetAllAsync();
            return ServiceResult<List<VehicleDto>>.Success(mapper.Map<List<VehicleDto>>(vehicles));
        }

        public async Task<ServiceResult<List<VehicleDto>>> GetPagedAllListAsync(int pagenumber, int pagesize)
        {
            var vehicles = await vehicleRepository.GetAll()
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return ServiceResult<List<VehicleDto>>.Success(mapper.Map<List<VehicleDto>>(vehicles));
        }

        public async Task<ServiceResult<CreateVehicleResponse>> CreateAsync(CreateVehicleRequest request)
        {
            var vehicle = mapper.Map<Vehicle>(request);
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateVehicleResponse>.SuccessAsCreated(
                new CreateVehicleResponse(vehicle.Id),
                $"api/vehicles/{vehicle.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateVehicleRequest request)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
                return ServiceResult.Fail($"Vehicle with id {id} not found", HttpStatusCode.NotFound);

            var existingVehicleWithSameNumberPlate = await vehicleRepository.GetByNumberPlateAsync(request.NumberPlate);
            if (existingVehicleWithSameNumberPlate is not null && existingVehicleWithSameNumberPlate.Id != id)
                return ServiceResult.Fail($"Vehicle with number plate {request.NumberPlate} already exists", HttpStatusCode.BadRequest);

            mapper.Map(request, vehicle);
            vehicleRepository.Update(vehicle);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
                return ServiceResult.Fail($"Vehicle with id {id} not found", HttpStatusCode.NotFound);

            vehicleRepository.Delete(vehicle);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}
