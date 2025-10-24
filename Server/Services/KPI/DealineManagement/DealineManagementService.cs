using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Data.Entities.KPI;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.KPI.DealineManagement;

namespace AquaSolution.Server.Services.KPi.FormulaService;

public class DealineManagementService : IDealineManagementService
{
    private readonly IRepository<KPIMonthlyActual> _kpiMonthlyActualRepo;
    private readonly IRepository<KPIRequest> _kpiRequestRepo;
    private readonly IRepository<KPITotalScore> _kpiTotalScoreRepo;
    private readonly IRepository<DealineKPISubmitManagement> _dealineKPISubmitManagementRepo;
    private readonly IRepository<KPIMonthlyTarget> _kpiMonthlyTargetRepo;
    public DealineManagementService(IRepository<KPIMonthlyActual> kpiMonthlyActualRepo,
        IRepository<DealineKPISubmitManagement> dealineKPISubmitManagementRepo,
         IRepository<KPIRequest> kpiRequestRepo,
         IRepository<KPITotalScore> kpiTotalScoreRepo,
    IRepository<KPIMonthlyTarget> kpiMonthlyTargetRepo)
    {
        _kpiMonthlyActualRepo = kpiMonthlyActualRepo;
        _dealineKPISubmitManagementRepo = dealineKPISubmitManagementRepo;
        _kpiMonthlyTargetRepo = kpiMonthlyTargetRepo;
        _kpiRequestRepo = kpiRequestRepo;
        _kpiTotalScoreRepo = kpiTotalScoreRepo;


    }
    public async Task<List<DealineManagementDto>> GetDealineManagement()
    {
        var now = DateTime.Now;

        var deadlines = await _dealineKPISubmitManagementRepo.GetAllAsync();
        var targets = await _kpiMonthlyTargetRepo.GetAllAsync();
        var actuals = await _kpiMonthlyActualRepo.GetAllAsync();
        var totalScores = await _kpiTotalScoreRepo.GetAllAsync();
        var requests = await _kpiRequestRepo.GetAllAsync();

        var eligibleMonths = deadlines
            .Where(d =>
                d.StartDate <= now &&
                d.EndDate >= now &&
                targets.Any(t => t.Month.HasValue && t.Year == d.Year && t.Month.Value == d.Month) &&
                !actuals.Any(a => a.Month.HasValue && a.Year.HasValue && a.Month.Value == d.Month && a.Year.Value == d.Year)
            )
            .OrderBy(d => d.Year)
            .ThenBy(d => d.Month)
            .ToList();

        var resultList = new List<DealineManagementDto>();

        foreach (var deadline in eligibleMonths)
        {
            int m = deadline.Month;
            int y = deadline.Year;
            bool allowDisplay = false;

            // Kiểm tra request của tháng trước
            if (m > 1)
            {
                var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;
                if (reqPrev != null && reqPrev.RequestStatus == StatusKPIRequestType.Rejected)
                {
                    deadline.Month = deadline.Month - 1;
                    // Nếu tháng trước bị Reject → hiển thị lại tháng trước
                    allowDisplay = true;
                }
            }

            // Nếu không hiển thị tháng trước, kiểm tra tháng hiện tại
            if (!allowDisplay)
            {
                var tsCurrent = totalScores.FirstOrDefault(ts => ts.Month == m && ts.Year == y);
                var reqCurrent = tsCurrent != null ? requests.FirstOrDefault(r => r.SubmitId == tsCurrent.SubmitId) : null;

                if (reqCurrent != null && reqCurrent.RequestStatus == StatusKPIRequestType.Rejected)
                {
                    // Nếu tháng hiện tại bị Reject → hiển thị lại tháng hiện tại
                    allowDisplay = true;
                }
                else if (reqCurrent != null && reqCurrent.RequestStatus == StatusKPIRequestType.Approved)
                {
                    // Nếu tháng hiện tại đã Approved → cho phép hiển thị tháng tiếp theo
                    allowDisplay = true;
                }
                else
                {
                    // Nếu chưa có request hoặc Pending → kiểm tra tháng trước
                    if (m > 1)
                    {
                        var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                        var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;
                        if (reqPrev != null && reqPrev.RequestStatus == StatusKPIRequestType.Approved)
                        {
                            // Nếu tháng trước đã Approved → cho phép hiển thị tháng hiện tại
                            allowDisplay = true;
                        }
                    }
                    else
                    {
                        // Tháng 1 của năm → cho phép hiển thị
                        allowDisplay = true;
                    }
                }
            }

            if (allowDisplay)
            {
                resultList.Add(new DealineManagementDto
                {
                    Id = deadline.Id,
                    Month = deadline.Month,
                    Year = deadline.Year,
                    StartDate = deadline.StartDate,
                    EndDate = deadline.EndDate,
                    CreatedDate = deadline.CreatedDate
                });
            }
            else
            {
                // Nếu không cho phép hiển thị, dừng khỏi loop
                break;
            }
        }

        return resultList;
    }


}
