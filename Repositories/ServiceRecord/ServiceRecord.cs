using App.Repositories.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Service
{
    public class ServiceRecord:BaseEntity<int>,IAuditEntity
    {
        [Key]
       public  int Id { get; set; }
        public int VehicleId { get; set; }
       public  DateTime ServiceDate { get; set; }
       public  string ProcessedBy { get; set; } = default!;
        public decimal Cost { get; set; } = default!;
       public   string Explanation { get; set; } = default!;
       public string? TechnicianName { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
