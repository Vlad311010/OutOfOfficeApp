using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Lists.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeesRepository employeeRepo;
        
        public Employee? Employee { get; private set; }

        public DetailsModel(IEmployeesRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await employeeRepo.GetById(id);
            if (Employee == null)
                return NotFound();

            return Page();
        }
            
    }
}
