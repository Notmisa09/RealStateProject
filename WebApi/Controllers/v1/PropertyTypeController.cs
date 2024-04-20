using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.API.PropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Queries.GetByIdPropertyType;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[SwaggerTag("Property Type Maintenance")]
public class PropertyTypeController : BaseApiController
{
    #region Commadns

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PropertyTypeAddDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Creating a property type",
        Description = "Receive the parameters to create a type of property")]
    public async Task<IActionResult> Post([FromQuery] CreatePropertyTypeCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("You must send the data correctly");
        }
        var response = await Mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, "Successfully created Type of property");
    }
        
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Update for a type of property",
        Description = "Receive the parameters to modify a type of property")]
    public async Task<IActionResult> Put([FromQuery] UpdatePropertyTypeCommand command, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("You must send the data correctly");
        }
        if(command.Id != id)
        {
            return BadRequest("You must send the data correctly");
        }
        await Mediator.Send(command);
        return NoContent();
    }
    
    // [Authorize(Roles = "Admin")]
    // [HttpDelete("{id}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [Consumes(MediaTypeNames.Application.Json)]
    // [SwaggerOperation(
    //     Summary = "Delete an improvement",
    //     Description = "Receive the parameters to delete an improvement")]
    //
    // public async Task<IActionResult> Delete(int id)
    // {
    //     await Mediator.Send(new DeleteImprovementsCommand { Id = id });
    //     return NoContent();
    // }

    #endregion

    #region Queries

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "List of type of property",
        Description = "Get a list of all type of property")]
    public async Task<IActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetAllPropertyTypeQuery()));
    }

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Get type of property by Id",
        Description = "Get a type of property by id of property")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetByIdPropertyTypeQuery { Id = id }));           
    }


    #endregion
}