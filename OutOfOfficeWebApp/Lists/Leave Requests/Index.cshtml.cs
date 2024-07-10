using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Lists.LeaveRequests
{
    public class IndexModel : PageModel
    {
        private readonly ILeaveRequestsRepository leaveRequestsRepo;
        private readonly IEmployeesRepository employeesRepo;

        public IEnumerable<LeaveRequest> LeaveRequests { get; private set; }
        
        public IEnumerable<SelectListItem> AbsenceReasonSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(ILeaveRequestsRepository leaveRequestsRepo, IEmployeesRepository employeesRepo)
        {
            this.leaveRequestsRepo = leaveRequestsRepo;
            this.employeesRepo = employeesRepo;
            AbsenceReasonSelectors = AbsenceReason.GetSelectList(-1, true);
            StatusSelectors = RequestStatus.GetSelectList(-1, true);
        }

        private async Task<IEnumerable<LeaveRequest>> GetRequests()
        {
            if (User.IsInManagerRole())
            {
                return await leaveRequestsRepo.All();
            }
            else
            {
                Employee currentEmployee = (await User.GetActiveEmployee(employeesRepo))!;
                return await leaveRequestsRepo.EmployeeReleatedRequest(currentEmployee.ID);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            LeaveRequests = await GetRequests();
            return Page();
        }

        public async Task OnGetFilterAsync(string id, int? AbsenceReasonFilter, int? StatusFilter)
        {
            SearchValue = id;
            AbsenceReasonSelectors.SetSelectedOption(AbsenceReasonFilter.ToString());
            StatusSelectors.SetSelectedOption(StatusFilter.ToString());

            LeaveRequests = await GetRequests();
            if (!string.IsNullOrWhiteSpace(id))
                LeaveRequests = LeaveRequests.Where(r => r.ID.ToString().Contains(id));

            LeaveRequests = LeaveRequests.Where(p =>
                    (AbsenceReasonFilter == null || p.AbsenceReasonId == AbsenceReasonFilter)
                    && (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
