using App.Repositories;
using App.Repositories.Service;
using App.Repositories.Services;
using App.Services.Servicess;
using App.Services.Servicess.Create;
using App.Services.Servicess.Update;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services
{
    public class ServiceRecordService(IServiceRecordRepository serviceRecordRepository,IUnitOfWork unitofwork,IMapper mapper ) : IServiceRecordService
    {
       

        public async Task<ServiceResult<ServiceRecordsDto>> GetByIdAsync(int id)
        {
            var service = await serviceRecordRepository.GetByIdAsync(id);
            if (service is null)
            {
                return ServiceResult<ServiceRecordsDto>.Fail($"Service with id {id} not found", HttpStatusCode.NotFound);
            }

            var dto = mapper.Map<ServiceRecordsDto>(service);
            return ServiceResult<ServiceRecordsDto>.Success(dto);
        }

        public async Task<ServiceResult<IEnumerable<ServiceRecordsDto>>> GetAllAsync()
        {
            var records = await serviceRecordRepository.GetAllAsync();
            var dtoList = mapper.Map<IEnumerable<ServiceRecordsDto>>(records);
            return ServiceResult<IEnumerable<ServiceRecordsDto>>.Success(dtoList);
        }

        public async Task<ServiceResult<IEnumerable<ServiceRecordsDto>>> GetByVehicleIdAsync(int vehicleId)
        {
            var records = await serviceRecordRepository.GetByVehicleIdAsync(vehicleId) as List<ServiceRecord>;
            if (records == null)
            {
                return ServiceResult<IEnumerable<ServiceRecordsDto>>.Fail("No records found for the given vehicle ID", HttpStatusCode.NotFound);
            }

            var dtoList = mapper.Map<IEnumerable<ServiceRecordsDto>>(records);
            return ServiceResult<IEnumerable<ServiceRecordsDto>>.Success(dtoList);
        }

        public async Task<ServiceResult<CreateServiceRecordResponse>> CreateAsync(CreateServiceRecordRequest request)
        {
            var entity = mapper.Map<ServiceRecord>(request);
            await serviceRecordRepository.AddAsync(entity);
            await unitofwork.SaveChangesAsync();

            var response = new CreateServiceRecordResponse(entity.Id);
            return ServiceResult<CreateServiceRecordResponse>.SuccessAsCreated(response, $"api/services/{entity.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateServiceRecordRequest request)
        {
            var entity = await serviceRecordRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return ServiceResult.Fail($"Service with id {id} not found", HttpStatusCode.NotFound);
            }

            mapper.Map(request, entity);
            serviceRecordRepository.Update(entity);
            await unitofwork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await serviceRecordRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return ServiceResult.Fail($"Service with id {id} not found", HttpStatusCode.NotFound);
            }

            serviceRecordRepository.Delete(entity);
            await unitofwork.SaveChangesAsync();

            return ServiceResult.Success();
        }

    }
}
