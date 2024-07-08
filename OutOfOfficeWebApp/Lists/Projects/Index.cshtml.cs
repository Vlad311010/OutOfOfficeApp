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
        
        public IEnumerable<Project> Projects { get; private set; }

        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> ProjectTypeSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IProjectsRepository projectsRepo)
        {
            this.projectsRepo = projectsRepo;
            StatusSelectors = ActiveStatus.GetSelectList(-1, true);
            ProjectTypeSelectors = ProjectType.GetSelectList(-1, true);
        }
        
        public async Task OnGetAsync()
        {
            Projects = await projectsRepo.All();
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
