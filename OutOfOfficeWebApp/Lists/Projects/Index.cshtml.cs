using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Lists.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        
        public IEnumerable<Project> Projects { get; private set; }

        public IndexModel(IProjectsRepository projectsRepo)
        {
            this.projectsRepo = projectsRepo;
        }
        
        public async Task OnGetAsync()
        {
            Projects = await projectsRepo.All();
        }

        public async Task OnGetFindAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Projects = await projectsRepo.All();
            else 
                Projects = await projectsRepo.Find(id);
        }
    }
}
