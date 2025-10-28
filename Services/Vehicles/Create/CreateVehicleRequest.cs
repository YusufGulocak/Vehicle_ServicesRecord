using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles.Create
{
    public record CreateVehicleRequest(string Name,string NumberPlate,string Brand, DateTime? Year);
    //{
    //}
    //public class CreateVehicleRequest
    //{
    //    public string Name { get; set; }
    //    public string NumberPlate { get; set; }
    //    public string Brand { get; set; }
    //    public DateTime Year { get; set; }
    //}
}
