using System.Net;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;

    public class GetAllPropertiesQuery : IRequest<Response<List<PropertyViewModel>>>
    {
    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Response<List<PropertyViewModel>>>
    {
        private readonly IPropertyService _service;

        public GetAllPropertiesQueryHandler(IPropertyService service)
        {
            _service = service;
        }

        public async Task<Response<List<PropertyViewModel>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _service.GeAllWithInclude();

            if (properties.Count == 0) throw new ExceptionsForApi("Properties not found", (int)HttpStatusCode.NotFound);

            return new Response<List<PropertyViewModel>>(properties);
        }

    }
    