using System.Text.Json.Serialization;

namespace RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos
{
    public class AuthenticateResponseJWT : AuthenticateBaseDto
    {
        public string JWTtoken { get; set; }

        [JsonIgnore]
        public string RefreshTokwn { get; set; }

    }
}
