using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.SellingTypes.Commands.CreateSellingTypes;


public class CreateSellingTypesCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "Sales type name")]
    public string SellingTypeName { get; set; } = null!;
    [SwaggerParameter(Description = "Description of the type of sale")]
    public string Description { get; set; } = null!;
}
    
public class CreateSellingTypesCommandHandler : IRequestHandler<CreateSellingTypesCommand, Response<Unit>>
{
    private readonly ISellingTypeRepository _repository;
    private readonly IMapper _mapper;

    public CreateSellingTypesCommandHandler(ISellingTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(CreateSellingTypesCommand command, CancellationToken cancellationToken)
    {
        var sellingType = _mapper.Map<Domain.Entities.SellingType>(command);
        await _repository.AddAsync(sellingType);
        return new Response<Unit>(Unit.Value);
    }
}