namespace VehicleManager.Models
{
    public class CreateModelViewModel
    {
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public List<VehicleMakeViewModel> VehicleMakes { get; set; }
    }
}
