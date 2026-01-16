using AquaSolution.Shared.HRMS;
using AquaSolution.Shared.ITSuport.Attachments;
using AquaSolution.Shared.ITSuport.RequestSuport;
using System.Net.Mail;

namespace AquaSolution.Server.Services.HRMS
{
    public interface IHRMSService
    {
        Task<bool> ImportExcelAsync(List<BonusYearDto> data);
    }
}
