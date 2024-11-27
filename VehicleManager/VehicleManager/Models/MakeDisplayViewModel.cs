namespace VehicleManager.Models
{
    public class MakeDisplayViewModel
    {
        public PagedResultViewModel<VehicleMakeViewModel> PagedResultViewModel { get; set; } = new PagedResultViewModel<VehicleMakeViewModel>();
        public string SortBy { get; set; } = "NameAsc";

    }
}
