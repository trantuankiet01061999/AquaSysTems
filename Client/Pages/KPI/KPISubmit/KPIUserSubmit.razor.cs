using AntDesign;
using AquaSolution.Client.Common;
using AquaSolution.Client.Components.KPI.KPISubmit;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.KPI.KPISubmit
{
    public partial class KPIUserSubmit
    {
        #region Declaration
        private UserDto? CurrenUser { get; set; }
        [Inject] private HttpClient? Http { get; set; }
        private SelectedKPISubmitModalrazor _selectedKpiSubmitModalrazor = new();
        private List<ViewKPITotalScoreDto> DataSource { get; set; } = new();
        Table<ViewKPITotalScoreDto>? TableRef;
        private ApprovalTaskModal ApprovalTaskModalRef = new();
        private HubConnection? _hubConnection;
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
            var result = await Http.GetFromJsonAsync<List<ViewKPITotalScoreDto>>
                ($"api/KPISubmit/get-kpi-total-score-by-userid/{CurrenUser.Id}");

            if (result is not null)
            {
                DataSource = result;
            }
            else
            {
                DataSource = new();
            }
            StateHasChanged();
        }
        #endregion
        #region Action
        private async Task SelectedKpi()
        {
          await  _selectedKpiSubmitModalrazor.ShowModal(CurrenUser!);
        }
        private async Task ViewAsync(ViewKPITotalScoreDto kPiTotalScoreDto )
        {
            var approvalInfo = new ApprovalInfo();
            approvalInfo.SubmitId = kPiTotalScoreDto.SubmitId;
            await ApprovalTaskModalRef.ShowModalAsync(approvalInfo, true);
        }
        #endregion
    }
}
