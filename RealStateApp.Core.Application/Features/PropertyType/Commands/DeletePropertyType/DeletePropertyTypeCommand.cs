using MediatR;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyType;

public class DeletePropertyTypeCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "Id of the property type to delete")]
    public int Id { get; set; }
}
public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, Response<Unit>>
{
    private readonly IPropertyTypeService _service;
    public DeletePropertyTypeCommandHandler(IPropertyTypeService service)
    {
        _service = service;
    }

    public async Task<Response<Unit>> Handle(DeletePropertyTypeCommand command, CancellationToken cancellationToken)
    {
        await _service.Remove(command.Id);
        return new Response<Unit>(Unit.Value);
    }

}