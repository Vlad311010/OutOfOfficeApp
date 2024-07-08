using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Lists.Employees
{
    public class AddModel : PageModel
    {
        private readonly IProjectsRepository projectsRepo;
        private readonly IEmployeesRepository employeesRepo;

        [BindProperty]
        public EmployeeViewModel Employee { get; set; }

        public IEnumerable<SelectListItem> HRSelectors { get; private set; }
        public IEnumerable<SelectListItem> SubdivisionSelectors { get; private set; }
        public IEnumerable<SelectListItem> PositionSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> RoleSelectors { get; private set; }

        public AddModel(IProjectsRepository projectsRepo, IEmployeesRepository employeesRepo)
        {
            this.projectsRepo = projectsRepo;
            this.employeesRepo = employeesRepo;
        }

        private async Task InitForm()
        {
            Employee = new EmployeeViewModel();
            Employee.StatusId = new ActiveStatus(ActiveStatusEnum.Active).Id;
            Employee.OutOfOfficeBalance = 0;

            IEnumerable<Employee> HRManagers = await employeesRepo.Where(e => e.Position == new Position(PositionEnum.HRManager));
            HRSelectors = HRManagers.Select(hr => new SelectListItem(hr.FullName, hr.ID.ToString(), hr.ID == Employee.PeoplePartnerId));
            SubdivisionSelectors = Subdivision.GetSelectList(Employee.SubdivisionId);
            PositionSelectors = Position.GetSelectList(Employee.PositionId);
            StatusSelectors = ActiveStatus.GetSelectList(Employee.StatusId);
            RoleSelectors = Role.GetSelectList(Employee.RoleId);
        }

        public async Task OnGetAsync()
        {
            await InitForm();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            Employee.StatusId = new ActiveStatus(ActiveStatusEnum.Active).Id;
            Employee.OutOfOfficeBalance = 0;
            Employee.RoleId = Position.AppropriateRole(Employee.PositionId).Id;
            // ModelState.Clear();
            if (!TryValidateModel(Employee) || Employee == null)
            {
                await InitForm();
                return Page();
            }

            employeesRepo.Add(Employee);
            await projectsRepo.Save();

            return RedirectToPage("/Employees/Index");
        }
            
    }
}
