using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
     public class PropertyViewModel
    {
        public string AgentId { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public  string PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public string  SellingType { get; set; }
        public int SellingTypeId { get; set; }
    }
}
