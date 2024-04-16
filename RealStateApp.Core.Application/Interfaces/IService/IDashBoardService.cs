using RealStateApp.Core.Application.ViewModels.DashBoard;

namespace RealStateApp.Core.Application.Interfaces.IService
{
    public interface IDashBoardService
    {
        Task<DashBoardViewModel> GetDashBoardInfo(DashBoardViewModel vm);
    }
}
