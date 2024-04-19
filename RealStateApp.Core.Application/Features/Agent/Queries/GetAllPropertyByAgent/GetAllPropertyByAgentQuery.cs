using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Agent.Queries.GetAllPropertyByAgent;

public class GetAllPropertyByAgentQuery : IRequest<Response<List<PropertyAddViewModel>>>
{
    [SwaggerParameter(Description = "You must enter the agent id of the properties")]

    public string IdAgent { get; set; }
}

public class GetAllPropertyByAgentQueryHandler : IRequestHandler<GetAllPropertyByAgentQuery, Response<List<PropertyAddViewModel>>>
{
    private readonly IPropertyService _service;

    public GetAllPropertyByAgentQueryHandler(IPropertyService service)
    {
        _service = service;
    }

    public async Task<Response<List<PropertyAddViewModel>>> Handle(GetAllPropertyByAgentQuery request,
        CancellationToken cancellationToken)
    {
        var properties = await _service.GeAllWithIncludeByAgent();
        if (properties.Count == 0) throw new ExceptionsForApi("Properties not found", (int)HttpStatusCode.NoContent);
        return new Response<List<PropertyAddViewModel>>(properties);
    }
}
