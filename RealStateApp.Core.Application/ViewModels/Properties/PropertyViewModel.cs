using RealStateApp.Core.Domain.Entities;
using System.Data;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
     public class PropertyViewModel
    {
        public string PropertyCode { get; set; }
        public int Id { get; set; }
        public int favprop {  get; set; }
        public string AgentId { get; set; }
        public decimal Meters { get; set; }
        public string FrontImage { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int BedroomsAmount { get; set; }
        public int BathroomsAmount { get; set; }
        public string Description { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public string PropertyTypeName { get; set; }
        public int PropertyTypeId { get; set; }
        public string  SellingTypeName { get; set; }
        public int SellingTypeId { get; set; }
    }
}
