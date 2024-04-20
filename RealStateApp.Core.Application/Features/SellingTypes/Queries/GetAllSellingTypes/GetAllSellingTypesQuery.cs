using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.SellingType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.SellingTypes.Queries.GetAllSellingTypes;


public class GetAllSellingTypesQuery : IRequest<Response<IEnumerable<SellingTypeDTO>>>
{
}

public class GetAllSellingTypesQueryHandler : IRequestHandler<GetAllSellingTypesQuery, Response<IEnumerable<SellingTypeDTO>>> 
{
    private readonly ISellingTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllSellingTypesQueryHandler(ISellingTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<SellingTypeDTO>>> Handle(GetAllSellingTypesQuery request, CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<List<SellingTypeDTO>>(await _repository.GetAllAsync());

        if (responseValue.Count == 0) throw new ExceptionsForApi("Type of sale not found",(int)HttpStatusCode.NoContent);

        return new Response<IEnumerable<SellingTypeDTO>>(responseValue);
    }
}
