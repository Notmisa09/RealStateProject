using RealStateApp.Core.Application.ViewModels.SellingTypes;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface ISellingTypeService : IGenericService<SellingTypeVeiwModel , SellingTypeAddViewModel , SellingType>
    {

    }
}
