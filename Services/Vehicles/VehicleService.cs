using App.Repositories;
using App.Repositories.Vehicles;
using App.Services.ExceptionHandler;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles
{
    public class VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork,IMapper mapper) : IVehicleService
    {



        public async Task<ServiceResult<List<Vehicle>>> GetVehiclesByCountAsync(int count)
        {
            var vehicles = await vehicleRepository.GetAll()
                .Take(count)
                .ToListAsync();

            return new ServiceResult<List<Vehicle>>()
            {
                Data = vehicles
            };
        }

        public async Task<ServiceResult<VehicleDto>> GetByIdAsync(int id)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
            {
                 return ServiceResult<VehicleDto>.Fail($"Vehicle with id {id} not found", System.Net.HttpStatusCode.NotFound);
            }
            var vehicleAsDto = mapper.Map<VehicleDto>(vehicle);

            return ServiceResult<VehicleDto>.Success(vehicleAsDto!);

        }
        public async Task<ServiceResult<CreateVehicleResponse>> CreateAsync(CreateVehicleRequest request)
        {
            var vehicle = mapper.Map<Vehicle>(request);

            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateVehicleResponse>.SuccessAsCreated(new CreateVehicleResponse(vehicle.Id),$"api/vehicles/{vehicle.Id }");
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
            {
                return ServiceResult.Fail($"Vehicle with id {id} not found", System.Net.HttpStatusCode.NotFound);
            }
            vehicleRepository.Delete(vehicle);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
        public async Task<ServiceResult> UpdateAsync(int id, UpdateVehicleRequest request)
        {
            //fast fail 
            // guard clauses
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
            {
                return ServiceResult.Fail($"Vehicle with id {id} not found", HttpStatusCode.NotFound);
            }

            var existingVehicleWithSameNumberPlate = await vehicleRepository.GetByNumberPlateAsync(request.NumberPlate);
            if (existingVehicleWithSameNumberPlate is not null && existingVehicleWithSameNumberPlate.Id != id)
            {
                return ServiceResult.Fail($"Vehicle with number plate {request.NumberPlate} already exists", HttpStatusCode.BadRequest);
            }

            //vehicle.Name = request.Name;
            //vehicle.Brand = request.Brand;
            //vehicle.Year = request.Year;
            //vehicle.NumberPlate = request.NumberPlate;

            vehicle=mapper.Map(request, vehicle);

            vehicleRepository.Update(vehicle);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();

        }
        public async Task<ServiceResult<List<VehicleDto>>> GetAllAsync()
        {
            var vehicles = await vehicleRepository.GetAllAsync();
            var vehiclesAsDto = mapper.Map<List<VehicleDto>>(vehicles);
            return ServiceResult<List<VehicleDto>>.Success(vehiclesAsDto);
        }
        public async Task<ServiceResult<List<VehicleDto>>> GetPagedAllListAsync(int pagenumber,int pagesize)
        {
            
            var vehicle = await vehicleRepository.GetAll().Skip((pagenumber-1)*pagesize).ToListAsync();
            var vehiclesAsDto = mapper.Map<List<VehicleDto>>(vehicle);
            return ServiceResult<List<VehicleDto>>.Success(vehiclesAsDto);

        }
       /* public async Task<ServiceResult> UpdateVehicleAsync(int id, UpdateVehicleRequest request)
        {
            var vehicle = await vehicleRepository.GetByIdAsync(id);
            if (vehicle is null)
            {
                return ServiceResult.Fail($"Vehicle with id {id} not found", HttpStatusCode.NotFound);
            }
            vehicle.Name = request.Name;
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }*/
       public async Task<ServiceResult<CreateVehicleResponse>> CreateVehicleAsync(CreateVehicleRequest request)
        {
            throw new CriticalException("This is a critical exception");
            var vehicle = new Vehicle
            {
                Name = request.Name,
                Brand = request.Brand,
                Year = (DateTime)request.Year,
                NumberPlate = request.NumberPlate
            };
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateVehicleResponse>.SuccessAsCreated(new CreateVehicleResponse(vehicle.Id), $"api/vehicles/{vehicle.Id}");
        }
    }
}
