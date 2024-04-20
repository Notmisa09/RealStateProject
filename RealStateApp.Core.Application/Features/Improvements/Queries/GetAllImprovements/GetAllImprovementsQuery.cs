using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.Improvements;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements;

public class GetAllImprovementsQuery : IRequest<Response<IEnumerable<ImprovementsDTO>>>
{
    
}


public class GetAllImprovementsQueryHandler : IRequestHandler<GetAllImprovementsQuery, Response<IEnumerable<ImprovementsDTO>>> 
{
    private readonly IimprovementsRepository _repository;
    private readonly IMapper _mapper;

    public GetAllImprovementsQueryHandler(IimprovementsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<ImprovementsDTO>>> Handle(GetAllImprovementsQuery request, CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<List<ImprovementsDTO>>(await _repository.GetAllAsync());

        if (responseValue.Count == 0) throw new ExceptionsForApi("Improvements not found",(int)HttpStatusCode.NotFound);

        return new Response<IEnumerable<ImprovementsDTO>>(responseValue);
    }
}
