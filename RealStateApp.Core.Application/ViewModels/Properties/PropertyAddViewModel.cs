using Microsoft.AspNetCore.Http;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class PropertyAddViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AgentId { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public int SellingTypeId { get; set; }
        public string SellingTypeName {  get; set; }
        public List<IFormFile>? formFile { get; set; }
    }
}
