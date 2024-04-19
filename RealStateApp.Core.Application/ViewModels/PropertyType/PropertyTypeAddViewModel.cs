using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.PropertyType
{
    public class PropertyTypeAddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite el nombre de un tipo de propiedad")]
        public string PropertyTypeName { get; set; }

        [Required(ErrorMessage = "Digite una descripcion")]
        public string Description { get; set; }

    }
}
