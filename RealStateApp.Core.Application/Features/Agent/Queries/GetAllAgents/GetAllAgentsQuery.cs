using MediatR;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.Agent.Queries.GetAllAgents;

public class GetAllAgentsQuery : IRequest<Response<IList<UserViewModel>>>
{
    
}

public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, Response<IList<UserViewModel>>>
{
    private readonly IUserService _service;
    public GetAllAgentsQueryHandler(IUserService service)
    {
        _service = service;
        
    }

    public async Task<Response<IList<UserViewModel>>> Handle(GetAllAgentsQuery request,
        CancellationToken cancellationToken)
    {
        var agent = await _service.GeAllByUsers(RolesEnum.Agent.ToString());
        if (agent == null) throw new ExceptionsForApi("Agents not found",(int) HttpStatusCode.NotFound);
        return new Response<IList<UserViewModel>>(agent);
    }
}