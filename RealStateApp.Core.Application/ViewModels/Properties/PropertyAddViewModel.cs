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
        [Range(1, double.MaxValue, ErrorMessage = "Porfavor ingrese un número valido")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de baños")]
        [Range(1, 10, ErrorMessage = "Porfavor ingrese un numero entre 1-10")]

        public int BedroomsAmount { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de habitaciones")]
        [Range(1, 10, ErrorMessage = "Porfavor ingrese un numero entre 1-10")]
        public int BathroomsAmount { get; set; }

        [Required(ErrorMessage = "Debe de indicar la cantidad de metros cuadrados")]
        [Range(1, double.MaxValue, ErrorMessage = "Porfavor ingrese un número valido")]
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
