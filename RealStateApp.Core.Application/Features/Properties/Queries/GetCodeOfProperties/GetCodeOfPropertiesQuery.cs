using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetCodeOfProperties;


public class GetCodeOfPropertiesQuery : IRequest<Response<List<PropertyAddViewModel>>>
{
    [SwaggerParameter(Description = "You must enter the id of the property")]
    public string Code { get; set; }
}

public class GetCodeOfPropertiesQueryHandler : IRequestHandler<GetCodeOfPropertiesQuery, Response<List<PropertyAddViewModel>>>
{
    private readonly IPropertyService _service;

    public GetCodeOfPropertiesQueryHandler(IPropertyService service)
    {
        _service = service;
    }

    public async Task<Response<List<PropertyAddViewModel>>> Handle(GetCodeOfPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _service.GetPropertyByCode(request.Code);

        if (properties.Count is 0) throw new ExceptionsForApi("Property not found", (int)HttpStatusCode.NotFound);

        return new Response<List<PropertyAddViewModel>>(properties);
    }

}