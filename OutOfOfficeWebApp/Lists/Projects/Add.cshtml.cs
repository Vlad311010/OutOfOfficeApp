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
    public class AddModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        private readonly IEmployeesRepository employeesRepo;

        public Project Project { get; private set; }
        public IEnumerable<SelectListItem> PMSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> ProjectTypeSelectors { get; private set; }

        public AddModel(IProjectsRepository projectsRepo, IEmployeesRepository employeesRepo)
        {
            this.projectsRepo = projectsRepo;
            this.employeesRepo = employeesRepo;
        }

        public async Task OnGetAsync()
        {
            Project = new Project();
            Project.StartDate = DateTime.Now;


            IEnumerable<Employee> ProjectManagers = await employeesRepo.Where(e => e.Position == new Position(PositionEnum.ProjectManager));
            PMSelectors = ProjectManagers.Select(pm => new SelectListItem(pm.FullName, pm.ID.ToString(), false));
            StatusSelectors = ActiveStatus.GetSelectList(1);
            ProjectTypeSelectors = ProjectType.GetSelectList(1);
        }

        public async Task<ActionResult> OnPostAsync(ProjectViewModel project)
        {
            
            if (!TryValidateModel(project) || project == null)
            {
                Console.WriteLine(ModelState.IsValid);
                return Page();
            }

            projectsRepo.Add(project);
            await projectsRepo.Save();

            return RedirectToPage("/Projects/Index");
        }
            
    }
}
