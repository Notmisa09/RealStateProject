using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.Improvements;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements;

public class UpdateImprovementsCommand : IRequest<Response<ImprovementsAddDTO>>
{
    [SwaggerParameter(Description = "Id Improvement")]
    public int Id { get; set; }
    [SwaggerParameter(Description = "New name of Improvements")]
    public string ImprovementName { get; set; } = null!;
    [SwaggerParameter(Description = "New description of Improvements")]
    public string Description { get; set; } = null!;
}

public class UpdateImprovementsCommandHandler : IRequestHandler<UpdateImprovementsCommand, Response<ImprovementsAddDTO>>
{
    private readonly IimprovementsRepository _repository;
    private readonly IMapper _mapper;

    public UpdateImprovementsCommandHandler(IimprovementsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<ImprovementsAddDTO>> Handle(UpdateImprovementsCommand command, CancellationToken cancellationToken)
    {
        var improvements = await _repository.GetByIdAync(command.Id);

        if (improvements is null) throw new ExceptionsForApi("Improvement not found",(int)HttpStatusCode.NotFound);

        improvements = _mapper.Map<Domain.Entities.Improvements>(command);

        await _repository.UpdateAsync(improvements, improvements.Id);
            
        var responseValue = _mapper.Map<ImprovementsAddDTO>(improvements);

        return new Response<ImprovementsAddDTO>(responseValue);
    }
}