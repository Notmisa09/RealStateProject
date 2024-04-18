using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public  class Improvements : BaseEntity
    {
        public int Id { get; set; }
        public string ImprovementName { get; set; }
        public string Description { get; set; }
        public ICollection<PropertyImprovements>? PropertyImprovements { get; set; }
    }
}
