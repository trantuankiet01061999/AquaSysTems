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

    public async Task<List<DealineManagementDto>> GetDealineManagement(Guid userId)
    {
        var now = DateTime.Now;
        var resultList = new List<DealineManagementDto>();

        var deadlines = await _dealineKPISubmitManagementRepo.GetAllAsync();
        var totalScores = (await _kpiTotalScoreRepo.GetAllAsync())
            .Where(ts => ts.CreatedBy == userId)
            .ToList();
        // Sắp xếp deadline theo thứ tự Year -> Month
        var orderedDeadlines = deadlines
            .OrderBy(d => d.Year)
            .ThenBy(d => d.Month)
            .ToList();

        foreach (var deadline in orderedDeadlines)
        {
            if (!(deadline.StartDate <= now && deadline.EndDate >= now))
                continue;
            // check tháng nào được approval thì bỏ ra
            var checktotalScoresApproval = totalScores.Any(x => x.Status == StatusKPIRequestType.Approved
            && x.Month == deadline.Month);
            if(checktotalScoresApproval) continue;
            int m = deadline.Month;
            int y = deadline.Year;
            var datareturn = new List<DealineManagementDto>();
            datareturn.Add(new DealineManagementDto
            {
                Id = deadline.Id,
                StartDate = deadline.StartDate,
                EndDate = deadline.EndDate,
                CreatedDate = deadline.CreatedDate,
                Year = deadline.Year,
                Month = deadline.Month
            }); 
            //Check WaitingForApproval
            var checktotalScores = totalScores.Any(x =>  x.Status == StatusKPIRequestType.WaitingForApproval);
                if (checktotalScores) return new List<DealineManagementDto>();
            //Check Status tháng trước đó 
            var monthBefore = totalScores
             .Where(x => x.Month == deadline.Month - 1)
             .OrderByDescending(x => x.CreatedDate) 
             .FirstOrDefault()?.Status == StatusKPIRequestType.Rejected;

            if (monthBefore)
            {
                var checkmonthBefore = orderedDeadlines.Any(x =>x.Month == deadline.Month - 1 && x.StartDate <= now && x.EndDate >= now);
                if (!checkmonthBefore)
                {
                    return new List<DealineManagementDto>();
                }
                else
                {
                    return datareturn.Where(x => x.Month == deadline.Month - 1).ToList();
                }
            }
            else
            {
                return datareturn.Where(x => x.Month == deadline.Month).ToList();
            }
        }


        return resultList;
    }

}
