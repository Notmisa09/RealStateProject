using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvements.Commands.CreateImprovements;

public class CreateImprovementsCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "Sales type name")]
    public string ImprovementName { get; set; }
    [SwaggerParameter(Description = "Description of the type of sale")]
    public string Description { get; set; }
}
    
public class CreateImprovementsCommandHandler : IRequestHandler<CreateImprovementsCommand, Response<Unit>>
{
    private readonly IimprovementsRepository _repository;
    private readonly IMapper _mapper;

    public CreateImprovementsCommandHandler(IimprovementsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(CreateImprovementsCommand command, CancellationToken cancellationToken)
    {
        var improvements = _mapper.Map<Domain.Entities.Improvements>(command);
        improvements = await _repository.AddAsync(improvements);
        return new Response<Unit>(Unit.Value);
    }
}