using AquaSolution.Shared.ApprovalFlows;

namespace AquaSolution.Server.Services.Administration.ApprovalFlowService
{
    public interface IApprovalFlowService
    {
        Task<List<ApprovalFlowDto>> GetListApprovalFlow();
        Task<bool> DeleteApprovalFlow(Guid ApprovalFlowId);
        Task<bool> CreatedApprovalFlow(ApprovalFlowDto ApprovalFlowDto);
        Task<bool> UpdateApprovalFlow(ApprovalFlowDto ApprovalFlowDto);
    }
}
