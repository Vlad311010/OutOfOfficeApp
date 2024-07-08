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

        public IEnumerable<LeaveRequest> LeaveRequests { get; private set; }
        // public IEnumerable<SelectListItem> EmployeeSelectors { get; private set; }
        public IEnumerable<SelectListItem> AbsenceReasonSelectors { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(ILeaveRequestsRepository leaveRequestsRepo)
        {
            this.leaveRequestsRepo = leaveRequestsRepo;
            AbsenceReasonSelectors = AbsenceReason.GetSelectList(-1, true);
            StatusSelectors = ActiveStatus.GetSelectList(-1, true);
        }

        public async Task OnGetAsync()
        {
            LeaveRequests = await leaveRequestsRepo.All();
        }

        public async Task OnGetFilterAsync(string id, int? AbsenceReasonFilter, int? StatusFilter)
        {
            SearchValue = id;
            AbsenceReasonSelectors.SetSelectedOption(AbsenceReasonFilter.ToString());
            StatusSelectors.SetSelectedOption(StatusFilter.ToString());

            if (string.IsNullOrWhiteSpace(id))
                LeaveRequests = await leaveRequestsRepo.All();
            else
                LeaveRequests = await leaveRequestsRepo.FindById(id);

            LeaveRequests = LeaveRequests.Where(p =>
                    (AbsenceReasonFilter == null || p.AbsenceReasonId == AbsenceReasonFilter)
                    && (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
