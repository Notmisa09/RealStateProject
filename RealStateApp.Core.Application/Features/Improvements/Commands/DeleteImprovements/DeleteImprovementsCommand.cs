using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvements.Commands.DeleteImprovements;

public class DeleteImprovementsCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "The id of the improvement that will be removed")]
    public int Id { get; set; }
}

public class DeleteImprovementsCommandHandler : IRequestHandler<DeleteImprovementsCommand, Response<Unit>>
{
    
    private readonly IimprovementsRepository _repository;
    private readonly IMapper _mapper;

    public DeleteImprovementsCommandHandler(IimprovementsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Response<Unit>> Handle(DeleteImprovementsCommand command, CancellationToken cancellationToken)
    {
        var improvements = await _repository.GetByIdAync(command.Id);

        if (improvements == null) throw new ExceptionsForApi("Improvement not found",(int)HttpStatusCode.NoContent);

        await _repository.RemoveAsync(improvements);
        return new Response<Unit>(Unit.Value);
    }
}