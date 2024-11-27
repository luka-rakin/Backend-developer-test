using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.Services.Dtos
{
    public class CreateModelRequest
    {
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
