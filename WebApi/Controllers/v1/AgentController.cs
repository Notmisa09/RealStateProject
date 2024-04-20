using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Agent.Commands.ChangeStatus;
using RealStateApp.Core.Application.Features.Agent.Queries.GetAllAgents;
using RealStateApp.Core.Application.Features.Agent.Queries.GetAllPropertyByAgent;
using RealStateApp.Core.Application.Features.Agent.Queries.GetPropertyByAgentId;
using RealStateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;
using RealStateApp.Core.Application.Features.Improvements.Queries.GetByIdImprovements;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.Presentation.API.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Agent Maintenance")]

public class AgentController : BaseApiController
{

    #region Commands


    [Authorize(Roles="Admin")]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
        Summary = "Change the status of an Agent user ",
        Description = "Receive the parameters to activate or deactivate an agent"
        )]
    public async Task<IActionResult> Patch([FromQuery] ChangeStatusCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("You must send the data correctly");
        }
        await Mediator.Send(command);
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
        Summary = "List of agents",
        Description = "Return a list of all agents"
        )]

    public async Task<IActionResult> Get()
    {
        return Ok(await Mediator.Send(new GetAllAgentsQuery()));
    }

    [Authorize(Roles = "Admin,Developer")]
    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
    Summary = "Get agent by Id",
    Description = "Return an agent by id")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await Mediator.Send(new GetAgentByIdQuery { Id = id }));
    }


    [Authorize(Roles = "Admin,Developer")]
    [HttpGet]
    [Route ("GetPropByAgentId/{AgentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(
    Summary = "Get properties by agent Id",
    Description = "Return a list of properties by its agent id")]
    public async Task<IActionResult> GetPropertiesByAgent(string AgentId)
    {
        return Ok(await Mediator.Send(new GetAllPropertyByAgentQuery { IdAgent = AgentId }));
    }
    #endregion

}