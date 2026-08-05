using CitizenPortal.DTO.Dashboard;

namespace CitizenPortal.BLL.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardAsync();
    }
}
