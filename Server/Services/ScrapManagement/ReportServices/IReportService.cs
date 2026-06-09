using AquaSolution.Shared.ReportDto;
using AquaSolution.Shared.ScrapManagement.Scrap;

namespace AquaSolution.Server.Services.ScrapManagetment.ReportServices
{
    public interface IReportService
    {
        Task<ReportPageDto> GetReportPageAsync(ReportFilterDto filter);
        Task<ReportSummaryDto> GetSummaryAsync(ReportFilterDto filter);
        Task<List<DepartmentReportDto>> GetDepartmentReportAsync(ReportFilterDto filter);
        Task<List<MaterialReportDto>> GetMaterialReportAsync(ReportFilterDto filter);
        Task<List<TrendPointDto>> GetTrendAsync(ReportFilterDto filter);
        Task<ApprovalStatusDto> GetApprovalStatusAsync(ReportFilterDto filter);
        Task<List<ApprovalPipelineDto>> GetPipelineAsync(ReportFilterDto filter);
    }
}