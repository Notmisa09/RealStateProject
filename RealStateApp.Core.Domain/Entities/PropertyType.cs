using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyType : BaseEntity
    {
        public int Id { get; set; }
        public string PropertyTypeName { get; set; }
        public string Description { get; set; }
        public ICollection<Properties>? Property { get; set; }
    }
}
