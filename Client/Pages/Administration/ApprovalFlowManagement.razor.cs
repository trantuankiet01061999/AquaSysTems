using AquaSolution.Client.Common;
using AquaSolution.Shared.CommonDto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using AquaSolution.Shared.ApprovalFlows;

namespace AquaSolution.Client.Pages.Administration
{
    public partial class ApprovalFlowManagement
    {
        #region Declaration
        [Inject] private HttpClient? Http { get; set; }
        private List<ApprovalFlowDto>? _listApprovalFlow = new();
        private List<ApprovalFlowGroupDto> _groupedList = new();

        // private ApprovalFlowModal ApprovalFlowModal = new ApprovalFlowModal();
        #endregion

        #region Innit
        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();

        }
        private async Task LoadDataAsync()
        {
            if (Http != null)
                _listApprovalFlow = await Http.GetFromJsonAsync<List<ApprovalFlowDto>>
                    ("api/ApprovalFlow/get-all");
            if (_listApprovalFlow != null)
                _groupedList = _listApprovalFlow
                    .GroupBy(x => new { x.PositionName, x.System })
                    .Select(group => new ApprovalFlowGroupDto
                    {
                        PositionName = group.Key.PositionName,
                        System = group.Key.System,
                        Items = group.Select(item => new ApprovalFlowItemDto
                        {
                            CurrentStep = item.CurrentStep,
                            NextStep = item.NextStep,
                            UserApproveName = item.UserApproveName,
                            ApprovalSettingType = item.ApprovalSettingType
                        }).ToList()
                    })
                    .ToList();
        }
        #endregion

        #region Action
        private async Task CreatedApprovalFlow()
        {
            // await ApprovalFlowModal.Showmodal(new ApprovalFlowDto(), false);
        }
        private async Task EditApprovalFlow(ApprovalFlowDto approvalFlowDto)
        {
            // await ApprovalFlowModal.Showmodal(ApprovalFlowDto, true);
        }
        private async Task DeleteAsync(ApprovalFlowDto approvalFlowDto)
        {
            var message = $"Are you sure you want to delete the ApprovalFlow \" {approvalFlowDto.Name} \" không?";
            var confirm = await MessageBox.Confirm(Modal, message.ToString());
            if (confirm)
            {
                var response = await Http.DeleteAsync($"api/ApprovalFlow/delete/{approvalFlowDto.Id}");
                await LoadDataAsync();
                var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (response.IsSuccessStatusCode)
                {
                    await Message.Success(content?.message ?? "Deleted successfully");
                }
                else
                {
                    await Message.Error(content?.message ?? "An unexpected error occurred");
                }
            }
            await InvokeAsync(StateHasChanged);
        }
        #endregion
    }
}
