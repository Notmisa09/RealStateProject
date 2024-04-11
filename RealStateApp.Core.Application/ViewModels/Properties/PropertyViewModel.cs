using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
     public class PropertyViewModel
    {
        public string Id { get; set; }
        public string AgentId { get; set; }
        public decimal Price { get; set; }
        public int RoomsAmount { get; set; }
        public int BathroomsAmount { get; set; }
        public string Description { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public  string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public string  SellingType { get; set; }
        public int SellingTypeId { get; set; }
    }
}
