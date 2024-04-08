using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Interfaces.IService;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
         Summary = "Type in your credentials to authenticate your user",
         Description = "This send you the JWT to authenticate inside the API"
         )]
        public async Task<IActionResult> AuthenticateAysnc(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAysncAPI(request));
        }
    }
}
