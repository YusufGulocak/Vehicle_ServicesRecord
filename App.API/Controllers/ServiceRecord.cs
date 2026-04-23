using App.Services.Servicess;
using App.Services;
using App.Services.Servicess.Create;
using App.Services.Servicess.Update;
using App.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRecordController(IServiceRecordService serviceRecordservice) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await serviceRecordservice.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await serviceRecordservice.GetByIdAsync(id));

        [HttpGet("vehicle/{vehicleId:int}")]
        public async Task<IActionResult> GetByVehicleId(int vehicleId) => CreateActionResult(await serviceRecordservice.GetByVehicleIdAsync(vehicleId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceRecordRequest request) => CreateActionResult(await serviceRecordservice.CreateAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceRecordRequest request) => CreateActionResult(await serviceRecordservice.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await serviceRecordservice.DeleteAsync(id));
    }
}
