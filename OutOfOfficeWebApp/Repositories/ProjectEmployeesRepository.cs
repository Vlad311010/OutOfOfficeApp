using app.Repositories;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Repositories
{
    public class ProjectEmployeesRepository : IProjectEmployeesRepository
    {
        private readonly AppDbContext context;
        public ProjectEmployeesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Project>> RelatedProjects(int employeeId)
        {
            return await context.Projects
                .Where(p => context.ProjectEmployees.Where(record => record.EmployeeId == employeeId).Select(r => r.ProjectId).Contains(p.ID))
                .Include(p => p.ProjectType)
                .ToListAsync();
        }
        public async Task<IEnumerable<Employee>> RelatedEmployees(int projectId)
        {
            return await context.Employees.Where(e => context.ProjectEmployees.Where(record => record.ProjectId == projectId).Select(r => r.EmployeeId).Contains(e.ID)).ToListAsync();
        }

        public async Task Add(int projectId, int employeeId)
        {
            await context.ProjectEmployees.AddAsync(new ProjectEmployees(projectId, employeeId));
        }

        public void Remove(int projectId, int employeeId)
        {
            context.ProjectEmployees.Remove(new ProjectEmployees(projectId, employeeId));
        }
    }
}
