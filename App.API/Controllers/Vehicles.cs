using App.Services;
using App.Services.Vehicles;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(IVehicleService vehicleService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await vehicleService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await vehicleService.GetByIdAsync(id));

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAllList(int pageNumber, int pageSize) => CreateActionResult(await vehicleService.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request) => CreateActionResult(await vehicleService.CreateAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleRequest request) => CreateActionResult(await vehicleService.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await vehicleService.DeleteAsync(id));
    }
}
