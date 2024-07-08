using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface ILeaveRequestsRepository
    {
        Task<IEnumerable<LeaveRequest>> All();
        Task<LeaveRequest> GetById(int requestId);
        Task<IEnumerable<LeaveRequest>> FindById(string id);
        Task<IEnumerable<LeaveRequest>> EmployeeReleatedRequest(int employeeId);
    }
}
