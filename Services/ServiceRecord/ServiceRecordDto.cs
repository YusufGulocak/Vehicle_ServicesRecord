using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Servicess
{
    public record ServiceRecordsDto(int id, int VehicleId,DateTime ServiceDate,string ProcessedBy,decimal Cost,string Explanation);
}
