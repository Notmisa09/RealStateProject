namespace RealStateApp.Core.Application.ViewModels.Location
{
    public class LocationResponse
    {
        public bool HasError { get; set; }
        public List<LocationViewModel> Data {  get; set; }
    }
}
