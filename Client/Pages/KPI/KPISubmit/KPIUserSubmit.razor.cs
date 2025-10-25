using AntDesign;
using AquaSolution.Client.Common;
using AquaSolution.Client.Components.KPI.KPISubmit;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.KPI.KPITasks;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.KPI.KPISubmit
{
    public partial class KPIUserSubmit
    {
        #region Declaration
        private UserDto? CurrenUser { get; set; }
        [Inject] private HttpClient? Http { get; set; }
        private SelectedKPISubmitModalrazor SelectedKPISubmitModalrazor = new();
        private List<ViewKPITotalScoreDto> DataSource { get; set; } = new();
        Table<ViewKPITotalScoreDto>? TableRef;
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
        private async Task SelectedKPI()
        {
          await  SelectedKPISubmitModalrazor.ShowModal(CurrenUser!);
        }
        private async Task ViewAsync(ViewKPITotalScoreDto kPITotalScoreDto )
        {
            //await LoadData();
        }
        #endregion
    }
}
