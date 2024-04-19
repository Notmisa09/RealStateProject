using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Agent.Commands.ChangeStatus;


public class ChangeStatusCommand : IRequest<Response<string>>
{
    [SwaggerParameter(Description = "You must enter the ID of the agent to update")]
    public string AgentId { get; set; }
    [SwaggerParameter(Description = "You must enter the status of the agent to update")]
    public bool Status { get; set; }
}
public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, Response<string>>
{
    private readonly IUserService _service;

    public ChangeStatusCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<Response<string>> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
    {
        var agent = await _service.GetById(command.AgentId);
        var changeStatus = await _service.ChangeUserStatus(agent);
        if(agent.HasError) throw new ExceptionsForApi("Agent not found", (int)HttpStatusCode.NoContent);
        return new Response<string>(agent.Id);  //Verificar valor que devuelve, para en caso de modificar
        
    }
}