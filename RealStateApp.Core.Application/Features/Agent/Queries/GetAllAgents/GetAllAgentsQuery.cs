using MediatR;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.Agent.Queries.GetAllAgents;

public class GetAllAgentsQuery : IRequest<Response<IList<UserViewModel>>>
{
    
}

public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, Response<IList<UserViewModel>>>
{
    //Invocar dependecia que represente los metodos de agente
    
    public GetAllAgentsQueryHandler()
    {
        
    }

    public async Task<Response<IList<UserViewModel>>> Handle(GetAllAgentsQuery request,
        CancellationToken cancellationToken)
    {
        return null;
    }
}