using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;


public class CreatePropertyTypeCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "Property type name")]
    public string PropertyTypeName { get; set; } = null!;
    [SwaggerParameter(Description = "Property type description")]
    public string Description { get; set; } = null!;
}
    
public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, Response<Unit>>
{
    private readonly IPropertyTypeRepository _repository;
    private readonly IMapper _mapper;

    public CreatePropertyTypeCommandHandler(IPropertyTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(CreatePropertyTypeCommand command, CancellationToken cancellationToken)
    {
        var responseValue = _mapper.Map<Domain.Entities.PropertyType>(command);
        await _repository.AddAsync(responseValue);
        return new Response<Unit>(Unit.Value);
    }
}