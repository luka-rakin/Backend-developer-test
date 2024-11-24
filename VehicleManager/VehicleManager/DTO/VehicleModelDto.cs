using VehicleManager.Models;

namespace VehicleManager.DTO
{
    public interface VehicleModelDto
    {
        public int Id { get; set; }
        public string VehicleMake {  get; set; }
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
