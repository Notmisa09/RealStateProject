using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.PropertyType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyType;


public class GetAllPropertyTypeQuery : IRequest<Response<IEnumerable<PropertyTypeDTO>>>
{
}
public class GetAllPropertyTypeQueryHandler : IRequestHandler<GetAllPropertyTypeQuery, Response<IEnumerable<PropertyTypeDTO>>> 
{
    private readonly IPropertyTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPropertyTypeQueryHandler(IPropertyTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Response<IEnumerable<PropertyTypeDTO>>> Handle(GetAllPropertyTypeQuery request, CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<List<PropertyTypeDTO>>(await _repository.GetAllAsync());

        if (responseValue.Count == 0) throw new ExceptionsForApi("Property type not found",(int)HttpStatusCode.NoContent);

        return new Response<IEnumerable<PropertyTypeDTO>>(responseValue);
    }
}