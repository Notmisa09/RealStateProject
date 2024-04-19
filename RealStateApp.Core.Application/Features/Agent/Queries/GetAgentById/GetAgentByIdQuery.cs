using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Agent.Queries.GetPropertyByAgentId;

public class GetAgentByIdQuery : IRequest<Response<SaveUserViewModel>>
{
    [SwaggerParameter(Description = "Debe colocar el id del paciente que quiere obtener")]
    public string AgentId { get; set; }
}
public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, Response<SaveUserViewModel>>
{
    private readonly IUserService _service;

    public GetAgentByIdQueryHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<Response<SaveUserViewModel>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _service.GetById(request.AgentId);
        if (agent is null) throw new ExceptionsForApi("Agent not found", (int)HttpStatusCode.NoContent);
        return new Response<SaveUserViewModel>(agent);
    }


}