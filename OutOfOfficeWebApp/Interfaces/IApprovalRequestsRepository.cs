using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IApprovalRequestsRepository
    {
        Task<IEnumerable<ApprovalRequest>> All();
        Task<ApprovalRequest> GetById(int requestId);
        Task<ApprovalRequest> GetByLeaveRequest(int leaveRequestId);
        Task<IEnumerable<ApprovalRequest>> FindById(string id);
        Task<IEnumerable<ApprovalRequest>> EmployeeReleatedRequest(int employeeId);
        Task<IEnumerable<ApprovalRequest>> ApproverReleatedRequest(int approverId);
        void Add(ApprovalRequest request);
        Task Save();
    }
}
