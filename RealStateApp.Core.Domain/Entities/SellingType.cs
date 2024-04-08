using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class SellingType : BaseEntity
    {
        public string SellingTypeName { get; set; }
        public string Description {  get; set; }
        public int? PropertiesAmount { get; set; }
        public ICollection<Properties>? Properties { get; set; }
    }
}
