using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Lists.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        
        public Project? Project { get; private set; }

        public DetailsModel(IProjectsRepository projectsRepo)
        {
            this.projectsRepo = projectsRepo;
        }
        
        public async Task OnGetAsync(int id)
        {
            Project = await projectsRepo.GetById(id);
        }
            
    }
}
