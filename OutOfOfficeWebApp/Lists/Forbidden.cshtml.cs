using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;

namespace OutOfOfficeWebApp.Lists
{
    public class ForbiddenModel : PageModel
    {
        private readonly IEmployeesRepository employeeRepo;
        

        public ForbiddenModel()
        {
        }
        
        public void OnGet()
        {
        }
    }
}
