namespace RealStateApp.Core.Domain.Entities
{
    public class Properties
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public int RoomsAmount { get; set; }
        public int BathroomsAmount { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        //NAV PROPERTIES
        public string AgentId { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public PropertyType? PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public SellingType ? SellingType { get; set; }
        public int SellingTypeId { get; set; }
        public ICollection<PropertyImprovements>? PropertyImprovements { get; set; }
    }
}
