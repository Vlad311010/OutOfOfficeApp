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
        private readonly IProjectEmployeesRepository projectEmployeesRepo;
        private readonly IProjectsRepository projectsRepo;

        public Employee Employee { get; private set; }

        public IEnumerable<SelectListItem> HRSelectors { get; private set; }
        public IEnumerable<SelectListItem> SubdivisionSelectors { get; private set; }
        public IEnumerable<SelectListItem> PositionSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> RoleSelectors { get; private set; }
        public IEnumerable<SelectListItem> ProjectSelectors { get; private set; }


        public List<int> AssignedProjects { get; set; } = new();

        public EditModel(IEmployeesRepository employeesRepo, IProjectsRepository projectsRepo, IProjectEmployeesRepository projectEmployeesRepo)
        {
            this.employeesRepo = employeesRepo;
            this.projectEmployeesRepo = projectEmployeesRepo;
            this.projectsRepo = projectsRepo;
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

            AssignedProjects = (await projectEmployeesRepo.RelatedProjects(id)).Select(p => p.ID).ToList();
            IEnumerable<Project> allProjects = await projectsRepo.All();
            ProjectSelectors = allProjects.Select(p => new SelectListItem("Project " + p.ID, p.ID.ToString(), true));
            ProjectSelectors.First().Selected = true;


            return Page();
        }

        public async Task<ActionResult> OnPostSaveAsync(EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return Page();

            Employee? employeeToUpdate = await employeesRepo.GetById(Employee.ID);

            if (employeeToUpdate == null)
                return NotFound();

            employeeToUpdate.FullName = Employee.FullName;
            employeeToUpdate.SubdivisionId = Employee.SubdivisionId;
            employeeToUpdate.PositionId = Employee.PositionId;
            employeeToUpdate.StatusId = Employee.StatusId;
            employeeToUpdate.PeoplePartnerId = Employee.PeoplePartnerId;
            employeeToUpdate.OutOfOfficeBalance = Employee.OutOfOfficeBalance;
            employeeToUpdate.Photo = Employee.Photo;
            employeeToUpdate.RoleId = Employee.RoleId;

            AssignedProjects = (await projectEmployeesRepo.RelatedProjects(employeeToUpdate.ID)).Select(p => p.ID).ToList();
            List<int> projectsToAssign = Employee.AssignedProjects.Except(AssignedProjects).ToList();
            List<int> projectsToUnassign = AssignedProjects.Except(Employee.AssignedProjects).ToList();
            foreach (var project in projectsToAssign)
            {
                await projectEmployeesRepo.Add(project, employeeToUpdate.ID);
            }
            foreach (var project in projectsToUnassign)
            {
                projectEmployeesRepo.Remove(project, employeeToUpdate.ID);
            }



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
