using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("Membership System")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
