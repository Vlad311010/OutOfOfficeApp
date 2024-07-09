using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Lists.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        private readonly IProjectEmployeesRepository projectEmployeeRepo;
        private readonly IAuthorizationService authorizationService;
        
        public Project? Project { get; private set; }

        public DetailsModel(IProjectsRepository projectsRepo, IProjectEmployeesRepository projectEmployeeRepo, IAuthorizationService authorizationService)
        {                                                     
            this.projectsRepo = projectsRepo;
            this.projectEmployeeRepo = projectEmployeeRepo;
            this.authorizationService = authorizationService;
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {

            List<int> allowedUsers = (await projectEmployeeRepo.RelatedEmployees(id)).Select(e => e.ID).ToList();
            var resource = new OwnedPageAuthorizationRequest { AllowedUsers = allowedUsers };
            var authorizationResult = await authorizationService.AuthorizeAsync(User, resource, "OwnerOrManager");
            if (!authorizationResult.Succeeded)
                return Forbid();

            Project = await projectsRepo.GetById(id);
            if (Project == null)
                return NotFound();

            return Page();
        }
            
    }
}
