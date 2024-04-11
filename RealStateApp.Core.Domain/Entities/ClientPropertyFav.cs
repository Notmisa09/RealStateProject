using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class ClientPropertyFav : BaseEntity
    {
        public string User {  get; set; }
        public string PropertyId { get; set; }
    }
}
