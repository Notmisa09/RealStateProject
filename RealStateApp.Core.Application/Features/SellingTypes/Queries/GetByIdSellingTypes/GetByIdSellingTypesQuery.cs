using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.SellingType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.SellingTypes.Queries.GetByIdSellingTypes;


public class GetByIdSellingTypesQuery : IRequest<Response<SellingTypeDTO>>
{
    [SwaggerParameter(Description = "Enter the id of the improvement to get")]
    public int Id { get; set; }
}


public class GetByIdSellingTypesQueryHandler : IRequestHandler<GetByIdSellingTypesQuery, Response<SellingTypeDTO>>
{
    private readonly ISellingTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdSellingTypesQueryHandler(ISellingTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<SellingTypeDTO>> Handle(GetByIdSellingTypesQuery request,
        CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<SellingTypeDTO>(await _repository.GetByIdAync(request.Id));

        return responseValue == null
            ? throw new ExceptionsForApi("Type of sale not found", (int)HttpStatusCode.NoContent)
            : new Response<SellingTypeDTO>(responseValue);
    }
}