using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos
{
    public class AuthenticationResponse : AuthenticateBaseDto
    {
        public string PhoneNumber { get; set; }
    }
}