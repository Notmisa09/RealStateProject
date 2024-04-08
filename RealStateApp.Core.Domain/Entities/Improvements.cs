using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public  class Improvements : BaseEntity
    {
        public string ImprovementName { get; set; }
        public string Description { get; set; }
        public ICollection<PropertyImprovements>? PropertyImprovements { get; set; }
    }
}
