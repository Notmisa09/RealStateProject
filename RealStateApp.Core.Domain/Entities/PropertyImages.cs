using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImages : BaseEntity
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int PropertyId { get; set;}
    }
}
