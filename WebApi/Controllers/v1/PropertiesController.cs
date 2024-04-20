using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealStateApp.Core.Application.Features.Properties.Queries.GetByIdProperties;
using RealStateApp.Core.Application.Features.Properties.Queries.GetCodeOfProperties;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[SwaggerTag("Properties Maintenance")]
public class PropertiesController : BaseApiController
{
    #region Queries

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Get all properties",
        Description = "Returns the list of all properties")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllPropertiesQuery()));
    }
    
    [Authorize(Roles = "Admin,Developer")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Get property by Id",
        Description = "Return all information about a property")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetByIdPropertiesQuery { Id = id }));           
    }
    
    
    [Authorize(Roles = "Admin,Developer")]
    [HttpGet("{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Get property by Id",
        Description = "Return all information about a property")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(await Mediator.Send(new GetCodeOfPropertiesQuery { Code = code }));           
    }
    


    #endregion
}