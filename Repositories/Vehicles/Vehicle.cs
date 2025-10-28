using App.Repositories.Service;
using App.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Vehicles
{
    public class Vehicle:BaseEntity<int>,IAuditEntity
    {
        public string Name { get; set; }=default!;
        public string Brand { get; set; }=default!;
        public DateTime Year { get; set; }
        public string NumberPlate { get; set; } = default!;
        public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
