using MediatR;
using RealStateApp.Core.Application.Interfaces.IService;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.SellingTypes.Commands.DeleteSellingTypes;


public class DeleteSellingTypesCommand : IRequest<Response<Unit>>
{
    [SwaggerParameter(Description = "Id of the sale type to delete")]
    public int Id { get; set; }
}
public class DeleteSellingTypesCommandHandler : IRequestHandler<DeleteSellingTypesCommand, Response<Unit>>
{
    private readonly ISellingTypeService _service;
    public DeleteSellingTypesCommandHandler(ISellingTypeService service)
    {
        _service = service;
    }

    public async Task<Response<Unit>> Handle(DeleteSellingTypesCommand command, CancellationToken cancellationToken)
    {
        await _service.Remove(command.Id);
        return new Response<Unit>(Unit.Value);
    }
 
}