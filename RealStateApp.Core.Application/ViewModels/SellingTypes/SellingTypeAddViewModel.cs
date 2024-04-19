using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.SellingTypes
{
    public class SellingTypeAddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite el nombre de un tipo de venta")]
        public string SellingTypeName { get; set; }
        
        [Required(ErrorMessage = "Digite una descripcion")]
        public string Description { get; set; }
    }
}
