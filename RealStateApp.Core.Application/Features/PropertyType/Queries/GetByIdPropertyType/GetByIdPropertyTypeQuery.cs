using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.PropertyType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetByIdPropertyType;


public class GetByIdPropertyTypeQuery : IRequest<Response<PropertyTypeDTO>>
{
    //Pasar indicadores a la api
    public int Id { get; set; }
}
public class GetByIdPropertyTypeQueryHandler : IRequestHandler<GetByIdPropertyTypeQuery, Response<PropertyTypeDTO>>
{
    private readonly IPropertyTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdPropertyTypeQueryHandler(IPropertyTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Response<PropertyTypeDTO>> Handle(GetByIdPropertyTypeQuery request, CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<PropertyTypeDTO>(await _repository.GetByIdAync(request.Id));

        return responseValue == null
            ? throw new ExceptionsForApi("Property type not found",(int)HttpStatusCode.NotFound)
            : new Response<PropertyTypeDTO>(responseValue);
    }
}