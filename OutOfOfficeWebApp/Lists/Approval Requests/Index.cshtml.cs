using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace OutOfOfficeWebApp.Lists.ApprovalRequests
{
    public class IndexModel : PageModel
    {
        private readonly IApprovalRequestsRepository approvalRequestsRepo;
        private readonly IEmployeesRepository employeesRepo;

        public IEnumerable<ApprovalRequest> ApprovalRequests { get; private set;


    }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }
        public IEnumerable<SelectListItem> ApproverSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IApprovalRequestsRepository approvalRequestsRepo, IEmployeesRepository employeesRepo)
        {
            this.approvalRequestsRepo = approvalRequestsRepo;
            this.employeesRepo = employeesRepo;
            StatusSelectors = RequestStatus.GetSelectList(-1, true);
        }

        private async Task SetApproverSelectors()
        {
            ApproverSelectors = (await employeesRepo
                .Where(e => e.RoleId == (int)RoleEnum.HRManager || e.RoleId == (int)RoleEnum.ProjectManager))
                .Select(e => new SelectListItem(e.FullName, e.ID.ToString(), false))
                .ToList()
                .Prepend(new SelectListItem("---", string.Empty, true));
        }

        public async Task OnGetAsync()
        {
            ApprovalRequests = await approvalRequestsRepo.All();
            await SetApproverSelectors();
        }

        public async Task OnGetFilterAsync(string id, int? StatusFilter, int? ApproverFilter)
        {
            await SetApproverSelectors();
            SearchValue = id;
            StatusSelectors.SetSelectedOption(StatusFilter.ToString());
            ApproverSelectors.SetSelectedOption(ApproverFilter.ToString());

            if (string.IsNullOrWhiteSpace(id))
                ApprovalRequests = await approvalRequestsRepo.All();
            else
                ApprovalRequests = await approvalRequestsRepo.FindById(id);

            ApprovalRequests = ApprovalRequests.Where(p =>
                     (ApproverFilter == null || p.ApproverId == ApproverFilter)
                     && (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
