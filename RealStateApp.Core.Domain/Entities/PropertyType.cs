using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyType : BaseEntity
    {
        public string PropertyTypeName { get; set; }
        public string Description { get; set; }
        public int? PropertiesAmount { get; set; }
        public ICollection<Properties>? Property { get; set; }
    }
}
