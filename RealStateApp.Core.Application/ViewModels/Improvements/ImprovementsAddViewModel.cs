using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Improvements
{
    public class ImprovementsAddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de indicar el nombre de la mejora")]
        public string ImprovementName { get; set; }
       
        [Required(ErrorMessage = "Debe de digitar una descripción")]
        public string Description { get; set; }
    }
}
