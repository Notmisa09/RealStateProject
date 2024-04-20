using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.Acccount.AuthenticateDtos;
using RealStateApp.Core.Application.Dto.Acccount.Register;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IService;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("Authentication")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "User login",
            Description ="Login for developer and administrator users"
            )]
        public async Task<IActionResult> Authentication([FromQuery]AuthenticationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("You must send the data correctly");
                }
                var response = await _service.AuthenticateAysncAPI(request);
                return response.HasError ? StatusCode(StatusCodes.Status401Unauthorized,response.Error) : Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Register_Dev")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary ="Creation of a user of type Developer",
            Description="Receive the parameters to create a developer user"
            )]

        public async Task<IActionResult> RegisterDev([FromQuery]RegisterRequest register)
        {
            try
            {
                var devRole = RolesEnum.Developer.ToString();
                if (!ModelState.IsValid)
                {
                    return BadRequest("You must send the data correctly");
                }
                var response = await _service.RegisterHighRolesUsers(register,devRole);
                return response.HasError ? BadRequest(response.Error) : StatusCode(StatusCodes.Status201Created, "User dev create successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Register_Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creation of a user of type Administrator",
            Description = "Receive the parameters to create an administrator user"
         )]
        public async Task<IActionResult> RegisterAdmin([FromQuery]RegisterRequest register)
        {
            try
            {
                var adminRole = RolesEnum.Developer.ToString();
                if (!ModelState.IsValid)
                {
                    return BadRequest("You must send the data correctly");
                }
                var response = await _service.RegisterHighRolesUsers(register,adminRole);
                return response.HasError ? BadRequest(response.Error) : StatusCode(StatusCodes.Status201Created, "User admin create successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
    
}
