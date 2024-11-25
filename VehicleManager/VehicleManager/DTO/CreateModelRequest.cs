using VehicleManager.Models;

namespace VehicleManager.DTO
{
    public class CreateModelRequest
    {
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
