using AntDesign;
using AquaSolution.Shared.ITSuport.RequestSuport;
using AquaSolution.Shared.KPI.KPISubmit;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace AquaSolution.Client.Components.KPI.KPISubmit
{
    public partial class HandleApprovalTask
    {
        #region Declaration
        [Inject] private HttpClient? Http { get; set; }
        private bool IsVisibleModal { get; set; } = false;
        private string ModalTitle {  get; set; }
        private ApprovalInfo ApprovalInfo { get; set; } = new ApprovalInfo();
        private Form<ApprovalInfo> formRef = new();
        #endregion
        #region Init
        public async Task ShowModal(ApprovalInfo approvalInfo)
        {
            ApprovalInfo = new ApprovalInfo();
            ApprovalInfo = approvalInfo;
            ModalTitle = "Rejected";
            if (approvalInfo.IsApproved)
            {
                ModalTitle = "Approval";
            }
            IsVisibleModal = true;
            await InvokeAsync(StateHasChanged);
        }
        #endregion
        private async Task SaveAsync()
        {
            var response = await Http.PutAsJsonAsync("api/kpiSubmit/update-status-request-kpi", ApprovalInfo);

            if (response.IsSuccessStatusCode)
            {

                await Message.Success("Update successfully !");

            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await Message.Error($"Update failed");

            }
            IsVisibleModal = false;
            await InvokeAsync(StateHasChanged);
        }
        private void CancelAsync()
        {
            IsVisibleModal = false;
            StateHasChanged();
        }
    }
}
