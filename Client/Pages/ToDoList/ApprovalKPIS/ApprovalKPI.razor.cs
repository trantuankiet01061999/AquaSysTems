using AntDesign;
using AquaSolution.Client.Common;
using AquaSolution.Client.Components.KPI.KPISubmit;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.ToDoList.ApprovalKPIS
{
    public partial class ApprovalKPI
    {
        #region Declaration
        private UserDto? CurrenUser { get; set; }
        [Inject] private HttpClient? Http { get; set; }
        private List<ViewKPIForApprovalDto> DataSource { get; set; } = new();
        private List<GroupViewKPIForApproval> _groupedList { get; set; } = new();
        Table<ViewKPITotalScoreDto>? TableRef;
        private ApprovalTaskModal ApprovalTaskModalRef = new();
        #endregion
        #region Init
        protected override async Task OnInitializedAsync()
        {
            await LoadCurrenUser();
            await LoadData();
        }

        private async Task LoadCurrenUser()
        {
            if (Http != null)
            {
                var currenUserClass = new CurrenUser(Http, AuthStateProvider);
                CurrenUser = await currenUserClass.LoadCurrenUser();
            }
        }

        private async Task LoadData()
        {
            if (Http == null || CurrenUser == null) return;

            var result = await Http.GetFromJsonAsync<List<ViewKPIForApprovalDto>>
                ($"api/KPISubmit/get-kpi-approval");
            var data = result.Where(x=>x.DecisionMaker == CurrenUser.Id && 
            x.EApprovalStatusType != EApprovalStatusType.Pending).ToList();
            _groupedList = new List<GroupViewKPIForApproval>
                {
                    new() { ApprovalStatusType = EApprovalStatusType.InReview, StatusName = "Pending" },
                    new() { ApprovalStatusType = EApprovalStatusType.Approval, StatusName = "Approval" },
                    new() { ApprovalStatusType = EApprovalStatusType.Rejected, StatusName = "Rejected" }
                };
            if (data != null)
            {
                foreach (var group in _groupedList)
                {
                    group.Items = data
                        .Where(x => x.EApprovalStatusType == group.ApprovalStatusType)
                        .OrderByDescending(x => x.CreatedDate)
                        .ToList();
                }
            }
        }
        #endregion
        #region Actions
        private async Task ApprovalAsync(ViewKPIForApprovalDto item)
        {
            
        }
        private async Task RejectedAsync(ViewKPIForApprovalDto item)
        {

        }
        private async Task ViewDetail(ViewKPIForApprovalDto item)
        {
            await ApprovalTaskModalRef.ShowModalAsync(item, true);
        }
        #endregion
    }
}
