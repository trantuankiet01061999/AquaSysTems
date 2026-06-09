using AquaSolution.Data.Data.Entities.Admin;
using AquaSolution.Data.Data.Entities.Scraps;
using AquaSolution.Data.Entities.Scraps;
using AquaSolution.Data.Repositories;
using AquaSolution.Shared.Enum.Scrap;
using AquaSolution.Shared.ReportDto;
using AquaSolution.Shared.ScrapManagement.Materials;
using AquaSolution.Shared.ScrapManagement.Scrap;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AquaSolution.Server.Services.ScrapManagetment.ReportServices
{

    public class ReportService : IReportService
    {
        private readonly IRepository<HistoryScrap> _scrapRepo;
        private readonly IRepository<HistoryScrapDetail> _detailRepo;
        private readonly IRepository<RequestApproval> _approvalRepo;
        private readonly IRepository<FlowApprovalScrap> _flowRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Department> _departmentRepo;

        // Inject thêm repo Department / Factory / User nếu project có
        // private readonly IRepository<Department> _deptRepo;

        public ReportService(
            IRepository<HistoryScrap> scrapRepo,
            IRepository<HistoryScrapDetail> detailRepo,
            IRepository<RequestApproval> approvalRepo,
            IRepository<FlowApprovalScrap> flowRepo,
            IRepository<User> userRepo,
             IRepository<Department> departmentRepo)
        {
            _scrapRepo = scrapRepo;
            _detailRepo = detailRepo;
            _approvalRepo = approvalRepo;
            _flowRepo = flowRepo;
            _userRepo = userRepo;
            _departmentRepo = departmentRepo;
        }

        private IQueryable<HistoryScrap> ApplyFilter(IQueryable<HistoryScrap> query, ReportFilterDto filter)
        {
            // Factory
            if (filter.FactoryId.HasValue)
                query = query.Where(x => x.FactoryId == filter.FactoryId.Value);

            // Thời gian — luôn dựa vào CreatedDate của HistoryScrap (bảng master)
            switch (filter.Period)
            {
                case FilterPeriod.Week:
                    {
                        var (start, end) = GetIsoWeekRange(filter.Year, filter.Week ?? 1);
                        query = query.Where(x => x.CreatedDate >= start && x.CreatedDate < end);
                        break;
                    }

                case FilterPeriod.Month:
                    query = query.Where(x =>
                        x.CreatedDate.Year == filter.Year &&
                        x.CreatedDate.Month == filter.Month);
                    break;

                case FilterPeriod.Year:
                    query = query.Where(x => x.CreatedDate.Year == filter.Year);
                    break;
            }

            return query;
        }

        // ─── Helper: kỳ trước ───────────────────────────────────────────────
        private static ReportFilterDto PreviousPeriod(ReportFilterDto filter)
        {
            var prev = new ReportFilterDto
            {
                FactoryId = filter.FactoryId,
                Period = filter.Period,
                Year = filter.Year,
                Month = filter.Month,
                Week = filter.Week
            };

            switch (filter.Period)
            {
                case FilterPeriod.Week:
                    if (filter.Week <= 1) { prev.Week = 52; prev.Year--; }
                    else prev.Week = filter.Week - 1;
                    break;

                case FilterPeriod.Month:
                    if (filter.Month <= 1) { prev.Month = 12; prev.Year--; }
                    else prev.Month = filter.Month - 1;
                    break;

                case FilterPeriod.Year:
                    prev.Year--;
                    break;
            }

            return prev;
        }


        // ────────────────────────────────────────────────────────────────────────────
        // GIẢI THÍCH GetIsoWeekRange
        // ────────────────────────────────────────────────────────────────────────────
        // Ngày 08/06/2026 (hôm nay) là tuần ISO 24 của năm 2026.
        //
        // Kiểm tra nhanh trong C#:
        //   ISOWeek.GetWeekOfYear(new DateTime(2026, 6, 8))  → 24
        //
        // GetIsoWeekRange(2026, 24) trả về:
        //   Start = 2026-06-08 (Thứ Hai)
        //   End   = 2026-06-15 (exclusive — không lấy)
        //
        // Filter: WHERE CreatedDate >= '2026-06-08' AND CreatedDate < '2026-06-15'
        // → Lấy đúng các đơn tạo trong tuần 24 (08/06 → 14/06/2026)
        // ────────────────────────────────────────────────────────────────────────────

        // ── Thêm vào ReportFilterDto (nếu chưa có) ───────────────────────────────────
        // public int? Week { get; set; }   ← số thứ tự tuần ISO, ví dụ: 24

        // ── Client gửi lên khi chọn filter Tuần ──────────────────────────────────────
        // api/report/page?Period=Week&Year=2026&Week=24
        //
        // Cách lấy số tuần hiện tại trong Blazor (razor.cs):
        //   using System.Globalization;
        //   int currentWeek = ISOWeek.GetWeekOfYear(DateTime.Today);  // → 24

        public async Task<ReportPageDto> GetReportPageAsync(ReportFilterDto filter)
        {
     
            var summary = await GetSummaryAsync(filter);
            var dept = await GetDepartmentReportAsync(filter);
            var material = await GetMaterialReportAsync(filter);
            var trend = await GetTrendAsync(filter);
            var approval = await GetApprovalStatusAsync(filter);
            var pipeline = await GetPipelineAsync(filter);

            return new ReportPageDto
            {
                Summary = summary,
                DepartmentReport = dept,
                MaterialReport = material,
                Trend = trend,
                ApprovalStatus = approval,
                Pipeline = pipeline
            };
        }
        // ─── Summary ────────────────────────────────────────────────────────
        public async Task<ReportSummaryDto> GetSummaryAsync(ReportFilterDto filter)
        {
            try
            {
                var query = ApplyFilter(_scrapRepo.Query(), filter);
                var prevQuery = ApplyFilter(_scrapRepo.Query(), PreviousPeriod(filter));

                var scraps = await query.ToListAsync();
                var prevScraps = await prevQuery.ToListAsync();

                var totalOrders = scraps.Count;
                var totalWeight = scraps.Sum(x => x.TotalAmount ?? 0);
                var confirmedWeight = scraps.Sum(x => x.ConfirmAmount ?? 0);

                var nowPending = scraps.Count(x => x.Status == StatusScrap.Pending);
                var overdue = scraps.Count(x =>
                    x.Status == StatusScrap.Pending &&
                    (DateTime.Now - x.CreatedDate).TotalDays > 3);

                var prevOrders = prevScraps.Count;
                var prevWeight = prevScraps.Sum(x => x.TotalAmount ?? 0);

                return new ReportSummaryDto
                {
                    TotalOrders = totalOrders,
                    TotalWeight = totalWeight,
                    ConfirmedWeight = confirmedWeight,
                    PendingOrders = nowPending,
                    OverduePendingOrders = overdue,
                    TotalOrdersChange = prevOrders == 0 ? 0 : Math.Round((totalOrders - prevOrders) / (double)prevOrders * 100, 1),
                    TotalWeightChange = prevWeight == 0 ? 0 : Math.Round((double)((totalWeight - prevWeight) / prevWeight) * 100, 1)
                };
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }

        // ─── Department Report ───────────────────────────────────────────────
        public async Task<List<DepartmentReportDto>> GetDepartmentReportAsync(ReportFilterDto filter)
        {
            try
            {
                var query = ApplyFilter(_scrapRepo.Query(), filter);

                // Group theo DepartmentId
                var grouped = await query
                    .GroupBy(x => x.DepartmentId)
                    .Select(g => new
                    {
                        DepartmentId = g.Key,
                        TotalOrders = g.Count(),
                        TotalWeight = g.Sum(x => x.TotalAmount ?? 0),
                        ConfirmedWeight = g.Sum(x => x.ConfirmAmount ?? 0)
                    })
                    .ToListAsync();

                // Lấy chi tiết để tính TotalQuantity
                var scrapIds = await query.Select(x => x.Id).ToListAsync();
                var details = await _detailRepo.Query()
                    .Where(d => scrapIds.Contains(d.ScrapHistoryId))
                    .GroupBy(d => d.ScrapHistoryId)
                    .ToListAsync();
                var department = _departmentRepo.Query();
                // Map sang DTO — tên phòng ban cần join thêm nếu có DepartmentRepo
                var result = grouped.Select(g => new DepartmentReportDto
                {
                    DepartmentId = g.DepartmentId,
                    DepartmentName = department.FirstOrDefault(x=>x.Id==g.DepartmentId).Name, 
                    TotalQuantity = 0, // tính bên dưới
                    TotalWeight = g.TotalWeight,
                    ConfirmedWeight = g.ConfirmedWeight,
                    StatusLabel = g.ConfirmedWeight / (g.TotalWeight == 0 ? 1 : g.TotalWeight) < 0.75m
                        ? "Cần xem xét" : "Bình thường"
                }).ToList();

                return result;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
           
        }

        // ─── Material Report ─────────────────────────────────────────────────
        public async Task<List<MaterialReportDto>> GetMaterialReportAsync(ReportFilterDto filter)
        {
            try
            {
                var query = ApplyFilter(_scrapRepo.Query(), filter);
                var scrapIds = await query.Select(x => x.Id).ToListAsync();


                var confirmedScrapIds = await query
                    .Where(x => x.Status == StatusScrap.Done)
                    .Select(x => x.Id)
                    .ToListAsync();

                var details = await _detailRepo.Query()
                    .Where(d => scrapIds.Contains(d.ScrapHistoryId))
                    .ToListAsync();

                var confirmedDetails = details.Where(d => confirmedScrapIds.Contains(d.ScrapHistoryId)).ToList();

                var grouped = details
                    .GroupBy(d => new { d.MaterialId, d.Code, d.Name, d.TYPE, d.Unit })
                    .Select(g => new MaterialReportDto
                    {
                        MaterialId = g.Key.MaterialId,
                        Code = g.Key.Code,
                        Name = g.Key.Name,
                        Type = g.Key.TYPE,
                        Unit = g.Key.Unit,
                        TotalQuantity = g.Sum(x => x.Quantity),
                        TotalWeight = g.Sum(x => x.TotalWeight),
                        ConfirmedWeight = confirmedDetails
                            .Where(x => x.MaterialId == g.Key.MaterialId)
                            .Sum(x => x.TotalWeight)
                    })
                    .OrderByDescending(x => x.TotalWeight)
                    .ToList();

                return grouped;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        // ─── Trend ──────────────────────────────────────────────────────────
        public async Task<List<TrendPointDto>> GetTrendAsync(ReportFilterDto filter)
        {
            try
            {
                var baseQuery = _scrapRepo.Query();
                if (filter.FactoryId.HasValue)
                    baseQuery = baseQuery.Where(x => x.FactoryId == filter.FactoryId.Value);

                List<TrendPointDto> result = new();

                switch (filter.Period)
                {
                    case FilterPeriod.Week:
                        {
                            // 7 ngày trong tuần
                            //for (int d = 0; d < 7; d++)
                            //{
                            //    var day = ISOWeek.ToDateTime(filter.Year, filter.Week!.Value, (DayOfWeek)(d + 1 > 6 ? 0 : d + 1));
                            //    var data = await baseQuery
                            //        .Where(x => x.CreatedDate.Date == day.Date)
                            //        .ToListAsync();
                            //    result.Add(new TrendPointDto
                            //    {
                            //        Label = day.ToString("ddd dd/MM"),
                            //        TotalOrders = data.Count,
                            //        TotalWeight = data.Sum(x => x.TotalAmount ?? 0),
                            //        ConfirmedWeight = data.Sum(x => x.ConfirmAmount ?? 0)
                            //    });
                            //}

                            var week = filter.Week ?? 1;

                            for (int d = 0; d < 7; d++)
                            {
                                var day = ISOWeek.ToDateTime(
                                    filter.Year,
                                    week,
                                    (DayOfWeek)(d + 1 > 6 ? 0 : d + 1)
                                );

                                var data = await baseQuery
                                    .Where(x => x.CreatedDate.Date == day.Date)
                                    .ToListAsync();

                                result.Add(new TrendPointDto
                                {
                                    Label = day.ToString("ddd dd/MM"),
                                    TotalOrders = data.Count,
                                    TotalWeight = data.Sum(x => x.TotalAmount ?? 0),
                                    ConfirmedWeight = data.Sum(x => x.ConfirmAmount ?? 0)
                                });
                            }

                            break;
                        }
                    case FilterPeriod.Month:
                        {
                            // 12 tháng của năm
                            for (int m = 1; m <= 12; m++)
                            {
                                var data = await baseQuery
                                    .Where(x => x.CreatedDate.Year == filter.Year && x.CreatedDate.Month == m)
                                    .ToListAsync();
                                result.Add(new TrendPointDto
                                {
                                    Label = $"T{m}",
                                    TotalOrders = data.Count,
                                    TotalWeight = data.Sum(x => x.TotalAmount ?? 0),
                                    ConfirmedWeight = data.Sum(x => x.ConfirmAmount ?? 0)
                                });
                            }
                            break;
                        }
                    case FilterPeriod.Year:
                        {
                            // 5 năm gần nhất
                            for (int y = filter.Year - 4; y <= filter.Year; y++)
                            {
                                var data = await baseQuery
                                    .Where(x => x.CreatedDate.Year == y)
                                    .ToListAsync();
                                result.Add(new TrendPointDto
                                {
                                    Label = y.ToString(),
                                    TotalOrders = data.Count,
                                    TotalWeight = data.Sum(x => x.TotalAmount ?? 0),
                                    ConfirmedWeight = data.Sum(x => x.ConfirmAmount ?? 0)
                                });
                            }
                            break;
                        }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        // ─── Approval Status ─────────────────────────────────────────────────
        public async Task<ApprovalStatusDto> GetApprovalStatusAsync(ReportFilterDto filter)
        {
            try
            {
                var query = ApplyFilter(_scrapRepo.Query(), filter);
                var scraps = await query.ToListAsync();

                return new ApprovalStatusDto
                {
                    Approved = scraps.Count(x => x.Status == StatusScrap.Approved),
                    Pending = scraps.Count(x => x.Status == StatusScrap.Pending),
                    Rejected = scraps.Count(x => x.Status == StatusScrap.Rejected)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        // ─── Pipeline ────────────────────────────────────────────────────────
        public async Task<List<ApprovalPipelineDto>> GetPipelineAsync(ReportFilterDto filter)
        {
            try
            {
                var query = ApplyFilter(_scrapRepo.Query(), filter);

                // Lấy các đơn đang pending hoặc mới nhất
                var scraps = await query
                    .Where(x => x.Status == StatusScrap.Pending || x.Status == StatusScrap.Approved || x.Status == StatusScrap.Rejected)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(20)
                    .ToListAsync();

                var scrapIds = scraps.Select(x => x.Id).ToList();
                var approvals = await _approvalRepo.Query()
                    .Where(a => scrapIds.Contains(a.HistoryScrapId))
                    .ToListAsync();

                var flows = await _flowRepo.Query()
                    .Where(f => scraps.Select(s => s.FactoryId).Contains(f.FactoryId))
                    .ToListAsync();

                return scraps.Select(s =>
                {
                    var scrapApprovals = approvals.Where(a => a.HistoryScrapId == s.Id).ToList();
                    var currentApproval = scrapApprovals.OrderByDescending(a => a.Step).FirstOrDefault();
                    var totalSteps = flows.Count(f => f.FactoryId == s.FactoryId && f.DepartmentId == s.DepartmentId);
                    var user = _userRepo.Query().FirstOrDefault(x=>x.Id == currentApproval.DecisionMaker);
                    var department = _departmentRepo.Query().FirstOrDefault(x => x.Id == s.DepartmentId);
                    return new ApprovalPipelineDto
                    {
                        ScrapId = s.Id,
                        Title = s.Title,
                        DepartmentName = department.Name ?? "", // thay bằng tên thật
                        CurrentStep = currentApproval?.Step ?? 0,
                        TotalSteps = totalSteps == 0 ? 1 : totalSteps,
                        DecisionMakerName = user.FullName ??"", 
                        CreatedDate = s.CreatedDate,
                        Status = s.Status
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        // ── Helper tính ngày đầu/cuối tuần ISO 8601 (Thứ Hai → Chủ Nhật) ─────────────
        private static (DateTime Start, DateTime End) GetIsoWeekRange(int year, int week)
        {
            // Ngày 4/1 luôn thuộc tuần ISO 1 của năm đó
            var jan4 = new DateTime(year, 1, 4);
            int offset = DayOfWeek.Monday - jan4.DayOfWeek; 
            var week1Start = jan4.AddDays(offset).Date;

            DateTime start = week1Start.AddDays((week - 1) * 7);
            DateTime end = start.AddDays(7); 
            return (start, end);
        }
    }

}

