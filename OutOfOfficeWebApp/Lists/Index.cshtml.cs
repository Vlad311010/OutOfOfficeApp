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
        private readonly IEmployeeRepository employeeRepo;
        

        [BindProperty]
        public int? EmployeeId { get; set; }

        public IndexModel(/*IEmployeeRepository employeeRepo*/)
        {
            this.employeeRepo = employeeRepo;
            // _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
           
            if (EmployeeId == null)
                ModelState.AddModelError(nameof(EmployeeId), "ID Field is required");
            else 
                ModelState.AddModelError(nameof(EmployeeId), "Invalid ID");
            return Page();
            /*
            Employee? employee = await employeeRepo.GetById(EmployeeId);
            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                Message = "Invalid login or password";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.ID.ToString()),
                new Claim(ClaimTypes.Role, employee.Role.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            Message = "OK";
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // success
            return LocalRedirect(Url.Page("/Public/Index"));
            */
        }
    }
}
