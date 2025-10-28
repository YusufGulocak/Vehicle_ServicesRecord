using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Vehicles
{ 
    public record VehicleDto (int Id, string Name, string Brand, DateTime Year, string NumberPlate);

    
}
