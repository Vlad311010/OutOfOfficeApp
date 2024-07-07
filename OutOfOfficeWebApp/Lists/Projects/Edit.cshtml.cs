using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Lists.Projects
{
    public class EditModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        private readonly IEmployeesRepository employeesRepo;

        public Project Project { get; private set; }

        public IEnumerable<SelectListItem> PMSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> ProjectTypeSelectors { get; private set; }

        public EditModel(IProjectsRepository projectsRepo, IEmployeesRepository employeesRepo)
        {
            this.projectsRepo = projectsRepo;
            this.employeesRepo = employeesRepo;
        }
        
        public async Task OnGetAsync(int id)
        {
            Project = await projectsRepo.GetById(id);
            IEnumerable<Employee> ProjectManagers = await employeesRepo.Where(e => e.Position == new Models.Enums.Position(Models.Enums.PositionEnum.ProjectManager));
            
            PMSelectors = ProjectManagers.Select(pm => new SelectListItem(pm.FullName, pm.ID.ToString(), pm.ID == Project.ProjectManagerId));
            StatusSelectors = ActiveStatus.GetSelectList(Project.StatusId);
            ProjectTypeSelectors = ProjectType.GetSelectList(Project.ProjectTypeId);
        }

        public async Task<ActionResult> OnPostSaveAsync(ProjectViewModel Project)
        {
            if (!ModelState.IsValid)
                return Page();

            Project? projectToUpdate = await projectsRepo.GetById(Project.ID);
            if (Project == null)
                return NotFound();

            projectToUpdate.StartDate = Project.StartDate;
            projectToUpdate.EndDate = Project.EndDate;
            projectToUpdate.ProjectTypeId = Project.ProjectTypeId;
            projectToUpdate.ProjectManagerId = Project.ProjectManagerId;
            projectToUpdate.Comment = Project.Comment;
            projectToUpdate.StatusId = Project.StatusId;

            try
            {
                await projectsRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (projectsRepo.GetById(Project.ID) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Projects/Index");
        }
            
    }
}
