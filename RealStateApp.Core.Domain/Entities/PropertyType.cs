using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyType : BaseEntity
    {
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public ICollection<Properties>? Property { get; set; }
    }
}
