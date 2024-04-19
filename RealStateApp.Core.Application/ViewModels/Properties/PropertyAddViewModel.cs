using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class PropertyAddViewModel 
    {
        public int? Id { get; set; } = 0;

        [Required(ErrorMessage = "No ha seleccionado una localizacion")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Digite un precio")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de baños")]
        public int BedroomsAmount { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de habitaciones")]
        public int BathroomsAmount { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de metros cuadrados")]
        public decimal Meters { get; set; }

        [Required(ErrorMessage = "Debe de escribir una poner una descripcion")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un tipo de propiedad")]

        public int PropertyTypeId { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un tipo de venta")]

        public int SellingTypeId { get; set; }
        public bool HasError {  get; set; }

        //NULLABLE PROPS
        public string? Error {  get; set; }
        public List<int>? Improvements { get; set; }
        public List<string>? ImprovementsName { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile>? formFile { get; set; }
        public string? PropertyTypeName { get; set; }
        public string? SellingTypeName { get; set; }
        public string? AgentEmail { get; set; }
        public string? AgentPhoneNumber { get; set; }
        public string? PropertyCode { get; set; }
        public string? FrontImage { get; set; }
        public string? AgentId { get; set; }
    }
}
