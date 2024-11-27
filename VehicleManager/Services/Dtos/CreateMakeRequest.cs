using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.Services.Dtos
{
    public class CreateMakeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Abrv { get; set; } = string.Empty;
    }
}
