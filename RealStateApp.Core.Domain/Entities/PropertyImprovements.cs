namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImprovements
    {
        public Improvements? Improvements { get; set; }
        public int ImprovementId { get; set; }
        public Properties? Property { get; set; }
        public string PropertyId { get; set; }
    }
}
