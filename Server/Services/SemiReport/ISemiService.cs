using AquaSolution.Shared.SemiReport;

namespace AquaSolution.Server.Services.SemiReport
{
    public interface ISemiService
    {
        Task<List<SemiReportDto>> GetAllAsync();
    }
}
