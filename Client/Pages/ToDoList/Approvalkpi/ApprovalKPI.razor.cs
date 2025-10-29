using AntDesign;
using AquaSolution.Client.Common;
using AquaSolution.Client.Components.KPI.KPISubmit;
using AquaSolution.Shared.Enum.KPIType;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using OneOf.Types;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.ToDoList.Approvalkpi
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
        private HubConnection? _hubConnection;
        private HandleApprovalTask HandleApproval = new();
        #endregion
        #region Init
        protected override async Task OnInitializedAsync()
        {
            await LoadCurrenUser();
            await LoadData();
            await InitSignalRAsync();
        }
        private async Task InitSignalRAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                   .WithUrl(Navigation.ToAbsoluteUri(Navigation.BaseUri + "signalrhub"))
                   .Build();
            _hubConnection.On("ReloadKPIForUserApproval", async () =>
            {
                await LoadData();
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
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
            var data = new List<ViewKPIForApprovalDto>();
            var result = await Http.GetFromJsonAsync<List<ViewKPIForApprovalDto>>
                ($"api/KPISubmit/get-kpi-approval");
            if (CurrenUser.Roles.Any(x => x.Name == "Admin"))
            {
                data = result;

            }
            else
            {
                data = result.Where(x => x.DecisionMaker == CurrenUser.Id &&
               x.EApprovalStatusType != EApprovalStatusType.Pending).ToList();
            }
            // var data = result;
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
                         .OrderByDescending(x => x.Month)
                         .ThenBy(x => x.Step)
                         .ToList();
                }
            }
        }
        #endregion
        #region Actions
        private async Task EvenCallback(ApprovalInfo approvalInfo)
        {
            if (approvalInfo == null) return;
            if (approvalInfo.IsApproved)
            {
                await ApprovalAsync(approvalInfo.SubmitId, approvalInfo.RequestTaskId, approvalInfo.DecisionMaker);
            }
            else
            {
                await RejectedAsync(approvalInfo.SubmitId, approvalInfo.RequestTaskId, approvalInfo.DecisionMaker);
            }
        }
        private async Task ApprovalAsync(Guid submitId, Guid requestTaskId, Guid? decisionMaker)
        {
            var approvalInfo = new ApprovalInfo();
            approvalInfo.SubmitId = submitId;
            approvalInfo.RequestTaskId = requestTaskId;
            if (CurrenUser.Roles.Any(x => x.Name == "Admin"))
            {
                approvalInfo.DecisionMaker = CurrenUser.Id;
            }
            else
            {
                approvalInfo.DecisionMaker = decisionMaker;
            }
            approvalInfo.IsApproved = true;
            await HandleApproval.ShowModal(approvalInfo);
        }
        private async Task RejectedAsync(Guid submitId, Guid requestTaskId, Guid? decisionMaker)
        {
            var approvalInfo = new ApprovalInfo();
            approvalInfo.SubmitId = submitId;
            approvalInfo.RequestTaskId = requestTaskId;
            if (CurrenUser.Roles.Any(x => x.Name == "Admin"))
            {
                approvalInfo.DecisionMaker = CurrenUser.Id;
            }
            else
            {
                approvalInfo.DecisionMaker = decisionMaker;
            }
            approvalInfo.IsApproved = false;
            await HandleApproval.ShowModal(approvalInfo);
        }
        private async Task ViewDetail(ViewKPIForApprovalDto item)
        {
            var approvalInfo = new ApprovalInfo();
            approvalInfo.SubmitId = item.SubmitId;
            approvalInfo.DecisionMaker = item.DecisionMaker;
            approvalInfo.RequestTaskId = item.Id;
            bool isView;
            if (item.EApprovalStatusType == EApprovalStatusType.InReview)
            {
                isView = false;
            }
            else
            {
                isView = true;
            }
            await ApprovalTaskModalRef.ShowModalAsync(approvalInfo, isView);
        }
        #endregion
    }
}
