using AquaSolution.Shared.SemiReport;

namespace AquaSolution.Server.Services.SemiReport.PcbReportService
{
    public interface IPcbReportService
    {
        Task<List<PcbReportDto>> GetAllAsync();
    }
}
