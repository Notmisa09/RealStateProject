using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImages : BaseEntity
    {
        public string ImageURL { get; set; }
        public string PropertyId { get; set;}
    }
}
