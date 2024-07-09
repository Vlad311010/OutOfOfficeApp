using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Utils;
using System.Reflection;

namespace OutOfOfficeWebApp.Lists.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        private readonly IProjectEmployeesRepository projectEmployeesRepo;
        private readonly IEmployeesRepository employeesRepo;


        public IEnumerable<Project> Projects { get; private set; }

        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> ProjectTypeSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IProjectsRepository projectsRepo, IProjectEmployeesRepository projectEmployeesRepo, IEmployeesRepository employeesRepo)
        {
            this.projectsRepo = projectsRepo;
            this.projectEmployeesRepo = projectEmployeesRepo;
            this.employeesRepo = employeesRepo;
            StatusSelectors = ActiveStatus.GetSelectList(-1, true);
            ProjectTypeSelectors = ProjectType.GetSelectList(-1, true);
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInManagerRole())
                Projects = await projectsRepo.All();
            else
            {
                Employee? currentEmployee = await User.GetActiveEmployee(employeesRepo);
                if (currentEmployee == null)
                    return Unauthorized();

                Projects = await projectEmployeesRepo.RelatedProjects(currentEmployee.ID);
            }

            return Page();
        }

        public async Task OnGetFilterAsync(string id, int? TypeFilter, int? StatusFilter)
        {
            SearchValue = id;
            ProjectTypeSelectors.SetSelectedOption(TypeFilter?.ToString());
            StatusSelectors.SetSelectedOption(StatusFilter?.ToString());

            if (string.IsNullOrWhiteSpace(id))
                Projects = await projectsRepo.All();
            else 
                Projects = await projectsRepo.FindById(id);

            Projects = Projects.Where(p => 
                (TypeFilter == null || p.ProjectTypeId == TypeFilter)
                && (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
