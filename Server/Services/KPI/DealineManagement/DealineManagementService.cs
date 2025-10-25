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

        // 🔹 Lọc những deadline hợp lệ (đang trong thời gian hiện tại)
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

            // 🔸 1️⃣ Kiểm tra tháng trước
            if (m > 1)
            {
                var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

                // Nếu tháng trước bị Reject và tháng đó đang trong hạn → hiển thị lại tháng trước
                if (reqPrev != null &&
                    reqPrev.RequestStatus == StatusKPIRequestType.Rejected &&
                    deadlines.Any(d => d.Month == m - 1 && d.Year == y && d.StartDate <= now && d.EndDate >= now))
                {
                    deadline.Month = m - 1;
                    allowDisplay = true;
                }
            }

            // 🔸 2️⃣ Nếu chưa được hiển thị, kiểm tra tháng hiện tại
            if (!allowDisplay)
            {
                var tsCurrent = totalScores.FirstOrDefault(ts => ts.Month == m && ts.Year == y);
                var reqCurrent = tsCurrent != null ? requests.FirstOrDefault(r => r.SubmitId == tsCurrent.SubmitId) : null;

                // Nếu tháng hiện tại bị Reject và đang trong hạn → hiển thị lại tháng hiện tại
                if (reqCurrent != null &&
                    reqCurrent.RequestStatus == StatusKPIRequestType.Rejected &&
                    deadline.StartDate <= now && deadline.EndDate >= now)
                {
                    allowDisplay = true;
                }
                // Nếu tháng hiện tại đã Approved → cho phép hiển thị tháng kế tiếp (nếu còn hạn)
                else if (reqCurrent != null &&
                         reqCurrent.RequestStatus == StatusKPIRequestType.Approval)
                {
                    var nextDeadline = deadlines.FirstOrDefault(d => d.Month == m + 1 && d.Year == y);
                    if (nextDeadline != null &&
                        nextDeadline.StartDate <= now && nextDeadline.EndDate >= now)
                    {
                        allowDisplay = true;
                    }
                }
                // Nếu chưa có request hoặc Pending → chỉ hiển thị nếu tháng trước đã approved và tháng hiện tại đang trong hạn
                else
                {
                    if (m > 1)
                    {
                        var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                        var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

                        if (reqPrev != null &&
                            reqPrev.RequestStatus == StatusKPIRequestType.Approval &&
                            deadline.StartDate <= now && deadline.EndDate >= now)
                        {
                            allowDisplay = true;
                        }
                    }
                    else
                    {
                        // Tháng 1 của năm → cho phép hiển thị nếu trong hạn
                        if (deadline.StartDate <= now && deadline.EndDate >= now)
                            allowDisplay = true;
                    }
                }
            }

            // 🔹 3️⃣ Thêm vào kết quả nếu đủ điều kiện
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
                // ❌ Nếu tháng này không hợp lệ thì dừng vòng lặp
                break;
            }
        }

        return resultList;
    }


}
