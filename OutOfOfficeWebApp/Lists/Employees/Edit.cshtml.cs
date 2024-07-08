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
    public class EditModel : PageModel
    {
        private readonly IEmployeesRepository employeesRepo;

        public Employee Employee { get; private set; }

        public IEnumerable<SelectListItem> HRSelectors { get; private set; }
        public IEnumerable<SelectListItem> SubdivisionSelectors { get; private set; }
        public IEnumerable<SelectListItem> PositionSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> RoleSelectors { get; private set; }

        public EditModel(IEmployeesRepository employeesRepo)
        {
            this.employeesRepo = employeesRepo;
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await employeesRepo.GetById(id);
            if (Employee == null)
                return NotFound();

            IEnumerable<Employee> HRManagers = await employeesRepo.Where(e => e.Position == new Position(PositionEnum.HRManager));
            
            HRSelectors = HRManagers.Select(hr => new SelectListItem(hr.FullName, hr.ID.ToString(), hr.ID == Employee.PeoplePartnerId));
            SubdivisionSelectors = Subdivision.GetSelectList(Employee.SubdivisionId);
            PositionSelectors = Position.GetSelectList(Employee.PositionId);
            StatusSelectors = ActiveStatus.GetSelectList(Employee.StatusId);
            RoleSelectors = Role.GetSelectList(Employee.RoleId);

            return Page();
        }

        public async Task<ActionResult> OnPostSaveAsync(EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return Page();

            Employee? employeeToUpdate = await employeesRepo.GetById(Employee.ID);
            if (employeeToUpdate == null)
                return NotFound();

            employeeToUpdate.ID = Employee.ID;
            employeeToUpdate.FullName = Employee.FullName;
            employeeToUpdate.SubdivisionId = Employee.SubdivisionId;
            employeeToUpdate.PositionId = Employee.PositionId;
            employeeToUpdate.StatusId = Employee.StatusId;
            employeeToUpdate.PeoplePartnerId = Employee.PeoplePartnerId;
            employeeToUpdate.OutOfOfficeBalance = Employee.OutOfOfficeBalance;
            employeeToUpdate.Photo = Employee.Photo;
            employeeToUpdate.RoleId = Employee.RoleId;

            try
            {
                await employeesRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (employeesRepo.GetById(Employee.ID) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Employees/Index");
        }
            
    }
}
