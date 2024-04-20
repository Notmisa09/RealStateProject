using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using RealStateApp.Core.Application.Dto.API.Improvements;
using RealStateApp.Core.Application.Features.Improvements.Commands.CreateImprovements;
using RealStateApp.Core.Application.Features.Improvements.Commands.DeleteImprovements;
using RealStateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements;
using RealStateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using RealStateApp.Core.Application.Features.Improvements.Queries.GetByIdImprovements;

namespace RealStateApp.Presentation.API.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Improvement Maintenance")]
public class ImprovementsController : BaseApiController
{
    #region Commadns

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ImprovementsAddDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Creating an improvement",
        Description = "Receive the parameters to create an improvement")]
    public async Task<IActionResult> Post([FromQuery] CreateImprovementsCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("You must send the data correctly");
        }
        var response = await Mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, "Successfully created Improvement");
    }
        
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Update for an improvement",
        Description = "Receive the parameters to modify an improvement")]
    public async Task<IActionResult> Put([FromQuery]UpdateImprovementsCommand command, int id)
    {
        if (id != command.Id)
        {
            return BadRequest("You must send the data correctly");
        }
        return Ok(await Mediator.Send(command));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Delete an improvement",
        Description = "Receive the parameters to delete an improvement")]

    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteImprovementsCommand { Id = id });
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
        Summary = "List of improvements",
        Description = "Get a list of all improvements")]
    public async Task<IActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetAllImprovementsQuery()));
    }

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Get Improvement by Id",
        Description = "Get an improvement by id of property")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await Mediator.Send(new GetByIdImprovementsQuery { Id = id }));           
    }


    #endregion
    
}