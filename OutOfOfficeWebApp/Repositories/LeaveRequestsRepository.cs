using app.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Repositories
{
    public class LeaveRequestsRepository : ILeaveRequestsRepository
    {
        private readonly AppDbContext context;
        public LeaveRequestsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> All()
        {
            return await context.LeaveRequests
                .Include(r => r.Employee)
                .Include(r => r.AbsenceReason)
                .Include(r => r.Status)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetById(int requestId)
        {
            return await context.LeaveRequests
                .Where(r => r.ID == requestId)
                .Include(r => r.Employee)
                .Include(r => r.AbsenceReason)
                .Include(r => r.Status)
                .SingleAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> FindById(string id)
        {
            return await context.LeaveRequests
                .Where(r => r.ID.ToString().Contains(id))
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> EmployeeReleatedRequest(int employeeId)
        {
            return await context.LeaveRequests
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.Employee)
                .Include(r => r.AbsenceReason)
                .Include(r => r.Status)
                .ToListAsync();
        }

        public int Add(LeaveRequestViewModel request)
        {
            EntityEntry<LeaveRequest> entity = context.LeaveRequests.Add(new LeaveRequest(request));
            context.SaveChanges();
            return entity.Property(e => e.ID).CurrentValue;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
