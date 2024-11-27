using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleManager.Models
{
    public class ModelDisplayViewModel
    {
        public PagedResultViewModel<VehicleModelViewModel> PagedResultViewModel { get; set; } = new PagedResultViewModel<VehicleModelViewModel>();
        public string SortBy { get; set; } = "NameAsc";
        public int? MakeId { get; set; } = null;
        public IEnumerable<SelectListItem> VehicleMakesSelectList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
