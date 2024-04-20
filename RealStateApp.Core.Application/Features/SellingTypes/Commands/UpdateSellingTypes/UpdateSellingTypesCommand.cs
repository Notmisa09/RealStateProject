using System.Net;
using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.API.SellingType;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.SellingTypes.Commands.UpdateSellingTypes;

public class UpdateSellingTypesCommand : IRequest<Response<SellingTypeAddDTO>>
{
    [SwaggerParameter(Description = "Id of the type of sale to modify")]
    public int Id { get; set; }
    [SwaggerParameter(Description = "Sales type name")]
    public string SellingTypeName { get; set; } = null!;
    [SwaggerParameter(Description = "Description of the type of sale")]
    public string Description { get; set; } = null!;
}
    
public class UpdateSellingTypesCommandHandler : IRequestHandler<UpdateSellingTypesCommand, Response<SellingTypeAddDTO>>
{
    private readonly ISellingTypeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSellingTypesCommandHandler(ISellingTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<SellingTypeAddDTO>> Handle(UpdateSellingTypesCommand command, CancellationToken cancellationToken)
    {
        var sellingType = await _repository.GetByIdAync(command.Id);

        if (sellingType is null) throw new ExceptionsForApi("Type of sale not found",(int)HttpStatusCode.NotFound);

        sellingType = _mapper.Map<Domain.Entities.SellingType>(command);

        await _repository.UpdateAsync(sellingType, sellingType.Id);
            
        var responseValue = _mapper.Map<SellingTypeAddDTO>(sellingType);

        return new Response<SellingTypeAddDTO>(responseValue);
    }
}