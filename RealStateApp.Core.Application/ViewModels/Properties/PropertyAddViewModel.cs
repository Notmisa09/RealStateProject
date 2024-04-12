using Microsoft.AspNetCore.Http;

namespace RealStateApp.Core.Application.ViewModels.Properties
{
    public class PropertyAddViewModel
    {
        public string? Id { get; set; }
        public string? AgentId { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int BedroomsAmount { get; set; }
        public int BathroomsAmount { get; set; }
        public string Description { get; set; }
        public string? AgentEmail { get; set; }
        public string? AgentPhoneNumber { get; set; }
        public int PropertyTypeId { get; set; }
        public string? SellingType { get; set; }
        public int SellingTypeId { get; set; }
        public List<int> Improvements { get; set; }
        public List<IFormFile>? formFile { get; set; }
    }
}
