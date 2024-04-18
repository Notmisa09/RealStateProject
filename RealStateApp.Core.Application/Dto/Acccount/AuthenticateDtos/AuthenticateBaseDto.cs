namespace RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos
{
    public class AuthenticateBaseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string ImageUrl { get; set; }
        public string LastName { get; set; }
        public string Identification {  get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; } 
    }
}
