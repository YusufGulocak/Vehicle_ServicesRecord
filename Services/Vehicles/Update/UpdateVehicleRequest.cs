using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles.Update;

    public record UpdateVehicleRequest(int Id,string Name, string NumberPlate, string Brand, DateTime Year);
    
   

