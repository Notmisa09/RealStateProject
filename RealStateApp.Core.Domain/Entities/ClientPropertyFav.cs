using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class ClientPropertyFav : BaseEntity
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        public int PropertyId { get; set; }
    }
}
