using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Filter
{
    public class FilterViewModel
    {
        [Range(1, double.MaxValue, ErrorMessage = "Plese type a valid number")]
        public decimal? MaxValue { get; set; }
  
        [Range(1, double.MaxValue, ErrorMessage = "Plese type a valid number")]
        public decimal? MinValue { get; set; }
        
        [Range(1, double.MaxValue, ErrorMessage = "Plese type a valid number")]
        public int ? BathroomAmount { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Plese type a valid number")]
        public int ? BedRoomAmount { get; set; }
        public int? PropertyType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Plese type a valid number")]
        public string PropertyCode { get; set; }
    }
}
