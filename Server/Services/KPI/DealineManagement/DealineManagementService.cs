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
    //public async Task<List<DealineManagementDto>> GetDealineManagement()
    //{
    //    var now = DateTime.Now;

    //    var deadlines = await _dealineKPISubmitManagementRepo.GetAllAsync();
    //    var targets = await _kpiMonthlyTargetRepo.GetAllAsync();
    //    var actuals = await _kpiMonthlyActualRepo.GetAllAsync();
    //    var totalScores = await _kpiTotalScoreRepo.GetAllAsync();
    //    var requests = await _kpiRequestRepo.GetAllAsync();

    //    var eligibleMonths = deadlines
    //        .Where(d =>
    //            d.StartDate <= now &&
    //            d.EndDate >= now &&
    //            targets.Any(t => t.Month.HasValue && t.Year == d.Year && t.Month.Value == d.Month) &&
    //            !actuals.Any(a => a.Month.HasValue && a.Year.HasValue && a.Month.Value == d.Month && a.Year.Value == d.Year)
    //        )
    //        .OrderBy(d => d.Year)
    //        .ThenBy(d => d.Month)
    //        .ToList();

    //    var resultList = new List<DealineManagementDto>();

    //    foreach (var deadline in eligibleMonths)
    //    {
    //        int m = deadline.Month;
    //        int y = deadline.Year;
    //        bool allowDisplay = false;

    //        // 1️⃣ Kiểm tra tháng trước
    //        if (m > 1)
    //        {
    //            var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
    //            var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

    //            // Nếu tháng trước bị Reject và đang trong hạn → hiển thị lại tháng trước
    //            if (reqPrev != null &&
    //                reqPrev.RequestStatus == StatusKPIRequestType.Rejected)
    //            {
    //                var prevDeadline = deadlines.FirstOrDefault(d => d.Month == m - 1 && d.Year == y);
    //                if (prevDeadline != null &&
    //                    prevDeadline.StartDate <= now && prevDeadline.EndDate >= now)
    //                {
    //                    deadline.Month = m - 1;
    //                    allowDisplay = true;
    //                }
    //                else
    //                {
    //                    // ❌ Tháng trước bị reject nhưng đã hết hạn → dừng
    //                    break;
    //                }
    //            }
    //        }

    //        // 2️⃣ Nếu chưa được hiển thị, kiểm tra tháng hiện tại
    //        if (!allowDisplay)
    //        {
    //            var tsCurrent = totalScores.FirstOrDefault(ts => ts.Month == m && ts.Year == y);
    //            var reqCurrent = tsCurrent != null ? requests.FirstOrDefault(r => r.SubmitId == tsCurrent.SubmitId) : null;

    //            if (reqCurrent != null &&
    //                reqCurrent.RequestStatus == StatusKPIRequestType.Rejected)
    //            {
    //                // Nếu tháng hiện tại bị Reject và đang trong hạn → cho phép nhập lại
    //                if (deadline.StartDate <= now && deadline.EndDate >= now)
    //                {
    //                    allowDisplay = true;
    //                }
    //                else
    //                {
    //                    // ❌ Hết hạn → không hiển thị
    //                    break;
    //                }
    //            }
    //            else if (reqCurrent != null &&
    //                     reqCurrent.RequestStatus == StatusKPIRequestType.Approval)
    //            {
    //                // Nếu tháng hiện tại đã Approved → hiển thị tháng kế tiếp nếu còn hạn
    //                var nextDeadline = deadlines.FirstOrDefault(d => d.Month == m + 1 && d.Year == y);
    //                if (nextDeadline != null &&
    //                    nextDeadline.StartDate <= now && nextDeadline.EndDate >= now)
    //                {
    //                    allowDisplay = true;
    //                }
    //            }
    //            else
    //            {
    //                // Pending hoặc chưa có request
    //                if (m > 1)
    //                {
    //                    var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
    //                    var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

    //                    if (reqPrev != null &&
    //                        reqPrev.RequestStatus == StatusKPIRequestType.Approval &&
    //                        deadline.StartDate <= now && deadline.EndDate >= now)
    //                    {
    //                        allowDisplay = true;
    //                    }
    //                }
    //                else
    //                {
    //                    // Tháng 1 → hiển thị nếu trong hạn
    //                    if (deadline.StartDate <= now && deadline.EndDate >= now)
    //                        allowDisplay = true;
    //                }
    //            }
    //        }

    //        // 3️⃣ Thêm vào kết quả nếu đủ điều kiện
    //        if (allowDisplay)
    //        {
    //            resultList.Add(new DealineManagementDto
    //            {
    //                Id = deadline.Id,
    //                Month = deadline.Month,
    //                Year = deadline.Year,
    //                StartDate = deadline.StartDate,
    //                EndDate = deadline.EndDate,
    //                CreatedDate = deadline.CreatedDate
    //            });
    //        }
    //        else
    //        {
    //            // ❌ Nếu không hợp lệ thì dừng
    //            break;
    //        }
    //    }

    //    return resultList;
    //}
    public async Task<List<DealineManagementDto>> GetDealineManagement(Guid userId)
    {
        var now = DateTime.Now;

        var deadlines = await _dealineKPISubmitManagementRepo.GetAllAsync();

        var targets = (await _kpiMonthlyTargetRepo.GetAllAsync())
            .Where(t => t.UserId == userId)
            .ToList();

        var actuals = (await _kpiMonthlyActualRepo.GetAllAsync())
            .Where(a => targets.Any(t => t.Id == a.KPIMonthlyTargetId))
            .ToList();

        var totalScores = (await _kpiTotalScoreRepo.GetAllAsync())
            .Where(ts => ts.CreatedBy == userId)
            .ToList();

        var requests = (await _kpiRequestRepo.GetAllAsync())
            .Where(r => r.CreatedBy == userId)
            .ToList();

        var eligibleMonths = deadlines
            .Where(d =>
                d.StartDate <= now &&
                d.EndDate >= now &&
                targets.Any(t => t.Month.HasValue && t.Year == d.Year && t.Month.Value == d.Month) &&
                !actuals.Any(a => a.Month.HasValue && a.Year.HasValue &&
                                  a.Month.Value == d.Month && a.Year.Value == d.Year))
            .OrderBy(d => d.Year)
            .ThenBy(d => d.Month)
            .ToList();

        var resultList = new List<DealineManagementDto>();

        foreach (var deadline in eligibleMonths)
        {
            int m = deadline.Month;
            int y = deadline.Year;
            bool allowDisplay = false;

            // 1️⃣ Kiểm tra tháng trước
            if (m > 1)
            {
                var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

                if (reqPrev != null && reqPrev.RequestStatus == StatusKPIRequestType.Rejected)
                {
                    var prevDeadline = deadlines.FirstOrDefault(d => d.Month == m - 1 && d.Year == y);
                    if (prevDeadline != null && prevDeadline.StartDate <= now && prevDeadline.EndDate >= now)
                    {
                        deadline.Month = m - 1;
                        allowDisplay = true;
                    }
                    else break;
                }
            }

            // 2️⃣ Kiểm tra tháng hiện tại
            if (!allowDisplay)
            {
                var tsCurrent = totalScores.FirstOrDefault(ts => ts.Month == m && ts.Year == y);
                var reqCurrent = tsCurrent != null ? requests.FirstOrDefault(r => r.SubmitId == tsCurrent.SubmitId) : null;

                if (reqCurrent != null && reqCurrent.RequestStatus == StatusKPIRequestType.Rejected)
                {
                    if (deadline.StartDate <= now && deadline.EndDate >= now)
                        allowDisplay = true;
                    else break;
                }
                else if (reqCurrent != null && reqCurrent.RequestStatus == StatusKPIRequestType.Approval)
                {
                    var nextDeadline = deadlines.FirstOrDefault(d => d.Month == m + 1 && d.Year == y);
                    if (nextDeadline != null && nextDeadline.StartDate <= now && nextDeadline.EndDate >= now)
                        allowDisplay = true;
                }
                else
                {
                    if (m > 1)
                    {
                        var tsPrev = totalScores.FirstOrDefault(ts => ts.Month == m - 1 && ts.Year == y);
                        var reqPrev = tsPrev != null ? requests.FirstOrDefault(r => r.SubmitId == tsPrev.SubmitId) : null;

                        if (reqPrev != null && reqPrev.RequestStatus == StatusKPIRequestType.Approval &&
                            deadline.StartDate <= now && deadline.EndDate >= now)
                            allowDisplay = true;
                    }
                    else
                    {
                        if (deadline.StartDate <= now && deadline.EndDate >= now)
                            allowDisplay = true;
                    }
                }
            }

            // 3️⃣ Thêm kết quả nếu đủ điều kiện
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
            else break;
        }

        return resultList;
    }

}
