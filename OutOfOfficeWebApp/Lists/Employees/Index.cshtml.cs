using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Utils;
using System.Reflection;

namespace OutOfOfficeWebApp.Lists.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeesRepository employeesRepo;

        public IEnumerable<Employee> Employees { get; private set; }

        public IEnumerable<SelectListItem> PositionSelectors { get; private set; }
        public IEnumerable<SelectListItem> SubdivisionSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IEmployeesRepository employeesRepo)
        {
            this.employeesRepo = employeesRepo;
            PositionSelectors = Position.GetSelectList(-1, true);
            SubdivisionSelectors = Subdivision.GetSelectList(-1, true);
            StatusSelectors = ActiveStatus.GetSelectList(-1, true);
        }

        public async Task OnGetAsync()
        {
            Employees = await employeesRepo.All();
        }

        public async Task OnGetFilterAsync(string name, int? PositionFilter, int? SubdivisionFilter, int? StatusFilter)
        {
            SearchValue = name;
            PositionSelectors.SetSelectedOption(PositionFilter.ToString());
            SubdivisionSelectors.SetSelectedOption(SubdivisionFilter.ToString());
            StatusSelectors.SetSelectedOption(StatusFilter.ToString());

            if (string.IsNullOrWhiteSpace(name))
                Employees = await employeesRepo.All();
            else
                Employees = await employeesRepo.FindByName(name);

            Employees = Employees.Where(p =>
                    (PositionFilter == null || p.PositionId == PositionFilter)
                    && (SubdivisionFilter == null || p.SubdivisionId == SubdivisionFilter)
                    && (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
