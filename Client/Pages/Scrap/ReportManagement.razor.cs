using AntDesign;
using AquaSolution.Shared.Enum.Scrap;
using AquaSolution.Shared.Factory;
using AquaSolution.Shared.ReportDto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.Scrap
{
    public partial class ReportManagement : ComponentBase, IAsyncDisposable
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IMessageService Message { get; set; } = default!;

        private ReportFilterDto _filter = new()
        {
            Period = FilterPeriod.Month,
            Year = DateTime.Now.Year,
            Month = DateTime.Now.Month,
            Week = ISOWeek.GetWeekOfYear(DateTime.Today)
        };

        private ReportPageDto? _data;
        private bool _loading;
        private bool _exporting;

        // Flag: charts đã được render vào DOM chưa (lần đầu sau khi có data)
        private bool _domReady;

        private List<FactoryDto> _factories = new();
        private List<int> _years = Enumerable.Range(DateTime.Now.Year - 4, 5).Reverse().ToList();

        public class OptionItem
        {
            public int Value { get; set; }
            public string Label { get; set; } = string.Empty;
        }

        private List<OptionItem> _months = Enumerable.Range(1, 12)
            .Select(m => new OptionItem { Value = m, Label = $"Tháng {m}" })
            .ToList();

        private int _monthVal = DateTime.Now.Month;

        // ─── Lifecycle ───────────────────────────────────────────────────────

        protected override async Task OnInitializedAsync()
        {
            await LoadFactoriesAsync();
            await LoadReportAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // _domReady = true sau render đầu tiên có data
            // Chỉ dùng để vẽ chart lần đầu tiên (OnInitializedAsync)
            // Các lần sau chart được vẽ lại trực tiếp từ LoadReportAsync
            if (_data is not null && !_domReady)
            {
                _domReady = true;
                await RenderChartsAsync();
            }
        }

        // ─── Data loading ────────────────────────────────────────────────────

        private async Task LoadFactoriesAsync()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<List<FactoryDto>>("api/factory/get-all");
                _factories = result ?? new();
            }
            catch
            {
                _factories = new();
            }
        }

        private async Task LoadReportAsync()
        {
            _loading = true;
            StateHasChanged();

            try
            {
                if (_filter.Period == FilterPeriod.Month)
                    _filter.Month = _monthVal;

                var query = BuildQueryString();
                _data = await Http.GetFromJsonAsync<ReportPageDto>($"api/report/page?{query}");
            }
            catch (Exception ex)
            {
                await Message.Error($"Lỗi tải báo cáo: {ex.Message}");
            }
            finally
            {
                _loading = false;
                StateHasChanged();
            }

            // Nếu DOM đã sẵn sàng (không phải lần load đầu tiên),
            // destroy chart cũ rồi vẽ lại ngay — không cần chờ OnAfterRenderAsync
            if (_domReady && _data is not null)
            {
                await RenderChartsAsync();
            }
        }

        private string BuildQueryString()
        {
            var parts = new List<string>
            {
                $"Period={_filter.Period}",
                $"Year={_filter.Year}"
            };

            if (_filter.FactoryId.HasValue)
                parts.Add($"FactoryId={_filter.FactoryId}");

            if (_filter.Period == FilterPeriod.Month && _filter.Month.HasValue)
                parts.Add($"Month={_filter.Month}");

            if (_filter.Period == FilterPeriod.Week && _filter.Week.HasValue)
                parts.Add($"Week={_filter.Week}");

            return string.Join("&", parts);
        }

        // ─── Event handlers ──────────────────────────────────────────────────

        private async Task OnFactoryChanged(FactoryDto? factory)
        {
            _filter.FactoryId = factory?.Id;
            await LoadReportAsync();
        }

        private async Task OnFilterChangedFilter(FilterPeriod value)
        {
            _filter.Period = value;

            if (value == FilterPeriod.Week && !_filter.Week.HasValue)
                _filter.Week = ISOWeek.GetWeekOfYear(DateTime.Today);

            await LoadReportAsync();
        }
        private async Task OnFilterChanged()
        {
            await LoadReportAsync();
        }

        private async Task ExportReport()
        {
            _exporting = true;
            try
            {
                // Lấy tên nhà máy đang chọn (nếu chưa chọn thì "Tất cả nhà máy")
                var factoryName = _filter.FactoryId.HasValue
                    ? _factories.FirstOrDefault(f => f.Id == _filter.FactoryId)?.Name ?? "Tất cả nhà máy"
                    : "Tất cả nhà máy";

                var query = BuildQueryString();
                var encodedName = Uri.EscapeDataString(factoryName);

                var response = await Http.GetAsync($"api/report/export?{query}&factoryName={encodedName}");

                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();

                    // Lấy tên file từ Content-Disposition header nếu có, không thì tự đặt
                    var fileName = response.Content.Headers.ContentDisposition?.FileNameStar
                                ?? response.Content.Headers.ContentDisposition?.FileName
                                ?? $"BaoCaoScrap_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                    await JS.InvokeVoidAsync("downloadFile", fileName,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", bytes);

                    await Message.Success("Xuất báo cáo thành công!");
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    await Message.Error($"Xuất file thất bại: {err}");
                }
            }
            catch (Exception ex)
            {
                await Message.Error($"Lỗi: {ex.Message}");
            }
            finally
            {
                _exporting = false;
            }
        }
        // ─── Charts ──────────────────────────────────────────────────────────

        private async Task RenderChartsAsync()
        {
            if (_data is null) return;

            // Destroy tất cả chart cũ trước khi vẽ lại
            // Quan trọng: tránh lỗi "Canvas is already in use"
            try { await JS.InvokeVoidAsync("destroyAllCharts"); } catch { }

            // Dept Pie
            var deptLabels = _data.DepartmentReport.Select(x => x.DepartmentName).ToArray();
            var deptWeights = _data.DepartmentReport.Select(x => (double)x.TotalWeight).ToArray();
            await JS.InvokeVoidAsync("renderPieChart", "deptPieChart", deptLabels, deptWeights);

            // Compare Bar (Trend: đăng ký vs xác nhận)
            var trendLabels = _data.Trend.Select(x => x.Label).ToArray();
            var trendWeight = _data.Trend.Select(x => (double)x.TotalWeight).ToArray();
            var trendConfirm = _data.Trend.Select(x => (double)x.ConfirmedWeight).ToArray();
            await JS.InvokeVoidAsync("renderCompareBarChart", "compareBarChart",
                trendLabels, trendWeight, trendConfirm);

            // Material Horizontal Bar (top 5)
            var top5 = _data.MaterialReport.Take(5).ToList();
            var matLabels = top5.Select(x => x.Name).ToArray();
            var matWeights = top5.Select(x => (double)x.TotalWeight).ToArray();
            await JS.InvokeVoidAsync("renderHBarChart", "matBarChart", matLabels, matWeights);

            // Trend Line (số đơn)
            var trendOrders = _data.Trend.Select(x => (double)x.TotalOrders).ToArray();
            await JS.InvokeVoidAsync("renderLineChart", "trendLineChart", trendLabels, trendOrders);

            // Status Doughnut
            await JS.InvokeVoidAsync("renderDoughnutChart", "statusDoughnutChart",
                new[] { "Đã duyệt", "Đang duyệt", "Từ chối" },
                new[] { _data.ApprovalStatus.Approved, _data.ApprovalStatus.Pending, _data.ApprovalStatus.Rejected });
        }

        public async ValueTask DisposeAsync()
        {
            try { await JS.InvokeVoidAsync("destroyAllCharts"); } catch { }
        }
    }
}
