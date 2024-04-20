using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dto.API.SellingType;
using RealStateApp.Core.Application.Features.Improvements.Commands.DeleteImprovements;
using RealStateApp.Core.Application.Features.SellingTypes.Commands.CreateSellingTypes;
using RealStateApp.Core.Application.Features.SellingTypes.Commands.DeleteSellingTypes;
using RealStateApp.Core.Application.Features.SellingTypes.Commands.UpdateSellingTypes;
using RealStateApp.Core.Application.Features.SellingTypes.Queries.GetAllSellingTypes;
using RealStateApp.Core.Application.Features.SellingTypes.Queries.GetByIdSellingTypes;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Presentation.API.Controllers.v1;

[ApiVersion("1.0")]
[ApiController]
[SwaggerTag("Selling Type Maintenance")]
public class SellingTypeController : BaseApiController
{
    #region Commadns

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellingTypeAddDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Create a type of sale",
        Description = "Receive the parameters to create a type of sale")]
    public async Task<IActionResult> Post([FromQuery] CreateSellingTypesCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("You must send the data correctly");
        }
        await Mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, "Successfully created Type of sale");
    }
        
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Update for a type of sale",
        Description = "Receive the parameters to modify a type of sale")]
    public async Task<IActionResult> Put([FromQuery] UpdateSellingTypesCommand command, int id)
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
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Delete an improvement",
        Description = "Receive the parameters to delete a type of sale")]

    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSellingTypesCommand { Id = id });
        return NoContent();
    }

    #endregion

    #region Queries

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "List of sales types",
        Description = "Get a list of all sales types")]
    public async Task<IActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetAllSellingTypesQuery()));
    }

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Get type of sale by Id",
        Description = "Get a type of sale by id of property")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetByIdSellingTypesQuery { Id = id }));           
    }


    #endregion
}