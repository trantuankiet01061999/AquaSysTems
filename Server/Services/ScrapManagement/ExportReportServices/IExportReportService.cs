using AquaSolution.Shared.ReportDto;

namespace AquaSolution.Server.Services.ScrapManagetment.ExportReportService
{
    public interface IExportReportService
    {
        /// <summary>
        /// Xuất báo cáo Scrap ra file .xlsx, trả về byte array.
        /// </summary>
        /// <param name="filter">Bộ lọc (factory, period, year, month, week)</param>
        /// <param name="factoryName">Tên nhà máy để hiển thị trong header</param>
        Task<byte[]> ExportAsync(ReportFilterDto filter, string factoryName);
    }
}
