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

        public IEnumerable<ApprovalRequest> ApprovalRequests { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IApprovalRequestsRepository approvalRequestsRepo, IEmployeesRepository employeesRepo)
        {
            this.approvalRequestsRepo = approvalRequestsRepo;
            this.employeesRepo = employeesRepo;
            StatusSelectors = ActiveStatus.GetSelectList(-1, true);
        }

        public async Task OnGetAsync()
        {
            ApprovalRequests = await approvalRequestsRepo.All();
        }

        public async Task OnGetFilterAsync(string id, int? StatusFilter)
        {
            SearchValue = id;
            StatusSelectors.SetSelectedOption(StatusFilter.ToString());

            if (string.IsNullOrWhiteSpace(id))
                ApprovalRequests = await approvalRequestsRepo.All();
            else
                ApprovalRequests = await approvalRequestsRepo.FindById(id);

            ApprovalRequests = ApprovalRequests.Where(p =>
                    (StatusFilter == null || p.StatusId == StatusFilter)
                ).ToList();

        }
    }
}
