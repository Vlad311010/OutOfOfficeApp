using app.Repositories;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Repositories
{
    public class ApprovalRequestsRepository : IApprovalRequestsRepository
    {
        private readonly AppDbContext context;

        public ApprovalRequestsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApprovalRequest>> All()
        {
            return await context.ApprovalRequests.ToListAsync();
        }

        public async Task<ApprovalRequest> GetById(int requestId)
        {
            return await context.ApprovalRequests
                .Where(r => r.ID == requestId)
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Include(r => r.Status)
                .SingleAsync();
        }
        
        public async Task<IEnumerable<ApprovalRequest>> FindById(string id)
        {
            return await context.ApprovalRequests
                .Where(r => r.ID.ToString().Contains(id))
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalRequest>> EmployeeReleatedRequest(int employeeId)
        {
            return await context.ApprovalRequests
                .Include(r => r.LeaveRequest)
                .Where(r => r.LeaveRequest.EmployeeId == employeeId)
                .Include(r => r.Approver)
                .Include(r => r.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApprovalRequest>> ApproverReleatedRequest(int approverId)
        {
            return await context.ApprovalRequests
                .Where(r => r.ApproverId == approverId)
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Include(r => r.Status)
                .ToListAsync();
        }
    }
}
