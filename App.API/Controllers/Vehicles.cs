 using App.Services;
using App.Services.Vehicles;
using App.Services.Vehicles.Create;
using App.Services.Vehicles.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request) => CreateActionResult(await vehicleService.CreateAsync(request));
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleRequest request) => CreateActionResult(await vehicleService.UpdateAsync(id, request));
        [HttpDelete("{id:int}")]
        
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAllList(int pagenumber, int pagesize) => CreateActionResult(await vehicleService.GetPagedAllListAsync(pagenumber, pagesize));  





    }
}

