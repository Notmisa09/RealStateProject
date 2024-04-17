using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class SellingType : BaseEntity
    {
        public int Id { get; set; }
        public string SellingTypeName { get; set; }
        public string Description {  get; set; }
        public ICollection<Properties>? Properties { get; set; }
    }
}
