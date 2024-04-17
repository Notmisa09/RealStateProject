using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImprovements : BaseEntity
    {
        public Improvements? Improvements { get; set; }
        public int ImprovementId { get; set; }
        public Properties? Property { get; set; }
        public int PropertyId { get; set; }
    }
}
