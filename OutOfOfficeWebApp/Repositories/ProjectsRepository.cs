using app.Repositories;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly AppDbContext context;
        public ProjectsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Project>> All()
        {
            return await context.Projects
                .Include(p => p.ProjectType)
                .Include(p => p.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> FindById(string id)
        {
            return await context.Projects.Where(p => p.ID.ToString().Contains(id))
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectManager)
                .Include(p => p.Status)
                .ToListAsync();
        }

        public async Task<Project?> GetById(int id)
        {
            return await context.Projects.Where(p => p.ID == id)
                .Include(p => p.ProjectType)
                .Include(p => p.ProjectManager)
                .Include(p => p.Status)
                .SingleAsync();
        }



        public void Add(ProjectViewModel project)
        {
            Project projectModel = new Project(project);
            context.Add(projectModel);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        /*public async Task<IEnumerable<Project?>> EmployeeRelated(Employee employee)
        {
            return await context.Projects.Where(p => context.ProjectEmployees.Where(record => record.EmployeeId == employee.ID).Select(p => p.ProjectId).Contains(p.ID)).ToListAsync();
        }*/
    }
}
