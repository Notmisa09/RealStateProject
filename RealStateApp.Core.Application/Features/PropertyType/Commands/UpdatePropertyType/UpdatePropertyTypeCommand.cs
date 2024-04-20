using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.PropertyType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;

public class UpdatePropertyTypeCommand : IRequest<Response<PropertyTypeAddDTO>>
{
    [SwaggerParameter(Description = "Id of the property type to modify")]
    public int Id { get; set; }
    [SwaggerParameter(Description = "New name of property type")]
    public string PropertyTypeName { get; set; } = null!;
    [SwaggerParameter(Description = "New description of property type")]
    public string Description { get; set; } = null!;
}

public class UpdateImprovementsCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, Response<PropertyTypeAddDTO>>
{
    private readonly IPropertyTypeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateImprovementsCommandHandler(IPropertyTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PropertyTypeAddDTO>> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
    {
        var propertyType = await _repository.GetByIdAync(command.Id);

        if (propertyType is null) throw new ExceptionsForApi("Property type not found", (int)HttpStatusCode.NotFound);

        propertyType = _mapper.Map<Domain.Entities.PropertyType>(command);

        await _repository.UpdateAsync(propertyType, propertyType.Id);

        var responseValue = _mapper.Map<PropertyTypeAddDTO>(propertyType);

        return new Response<PropertyTypeAddDTO>(responseValue);
    }
}