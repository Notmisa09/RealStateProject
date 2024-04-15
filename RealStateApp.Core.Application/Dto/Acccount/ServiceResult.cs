namespace RealStateApp.Core.Application.Dto.Acccount
{
    public class ServiceResult 
    {
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
        public dynamic? T { get; set; }

    }
}
