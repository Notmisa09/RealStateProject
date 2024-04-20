using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetByIdProperties;

public class GetByIdPropertiesQuery : IRequest<Response<List<PropertyAddViewModel>>>
{
    [SwaggerParameter(Description = "You must enter the id of the property")]
    public int Id { get; set; }
}

public class GetByIdPropertiesQueryHandler : IRequestHandler<GetByIdPropertiesQuery, Response<List<PropertyAddViewModel>>>
{
    private readonly IPropertyService _service;

    public GetByIdPropertiesQueryHandler(IPropertyService service)
    {
        _service = service;
    }

    public async Task<Response<List<PropertyAddViewModel>>> Handle(GetByIdPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _service.GetPropertyById(request.Id);

        if (properties.Count == 0) throw new ExceptionsForApi("Property not found", (int)HttpStatusCode.NotFound);

        return new Response<List<PropertyAddViewModel>>(properties);
    }

}
