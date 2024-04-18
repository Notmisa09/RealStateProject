using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class PropertyAddViewModel
    {
        public int Id { get; set; } = -1;
        [Required(ErrorMessage = "No ha seleccionado una localizacion")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Digite un precio")]
        public decimal Price { get; set; }
        public int BedroomsAmount { get; set; }
        public int BathroomsAmount { get; set; }
        public decimal Meters { get; set; }
        public string Description { get; set; }
        public int PropertyTypeId { get; set; }
        public int SellingTypeId { get; set; }

        //NULLABLE PROPS

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
