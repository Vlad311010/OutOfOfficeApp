using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Models;
using System.Security.Claims;
using OutOfOfficeWebApp.Interfaces;

namespace OutOfOfficeWebApp.Lists
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeesRepository employeeRepo;
        

        [BindProperty]
        public int? EmployeeId { get; set; }

        public Employee? Employee { get; private set; }

        public IndexModel(IEmployeesRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }
        
        public async Task OnGetAsync()
        {
            int identificator;
            if (User.Identity!.IsAuthenticated && Int32.TryParse(HttpContext.User?.FindFirstValue("Identificator"), out identificator))
            {
                Employee = await employeeRepo.GetById(identificator);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployeeId == null)
                ModelState.AddModelError(nameof(EmployeeId), "ID Field is required");

            int id = EmployeeId ?? default;
            Employee? employee = await employeeRepo.GetById(id);
            if (employee == null) {
                ModelState.AddModelError(nameof(EmployeeId), "Invalid ID");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(type: "Identificator", value: employee.ID.ToString()),
                new Claim(ClaimTypes.Role, employee.Role.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // success
            return Redirect("~/");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }
    }
}
