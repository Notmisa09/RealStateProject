using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.Improvements;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvements.Queries.GetByIdImprovements;


public class GetByIdImprovementsQuery : IRequest<Response<ImprovementsDTO>>
{
    [SwaggerParameter(Description = "Enter the id of the improvement to get")]
    public int Id { get; set; }
}


public class GetByIdImprovementsQueryHandler : IRequestHandler<GetByIdImprovementsQuery, Response<ImprovementsDTO>>
{
    private readonly IimprovementsRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdImprovementsQueryHandler(IimprovementsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<ImprovementsDTO>> Handle(GetByIdImprovementsQuery request,
        CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<ImprovementsDTO>(await _repository.GetByIdAync(request.Id));

        return responseValue == null
            ? throw new ExceptionsForApi("Improvements not found", (int)HttpStatusCode.NotFound)
            : new Response<ImprovementsDTO>(responseValue);
    }
}