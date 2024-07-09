using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Lists.ApprovalRequests
{
    public class IndexModel : PageModel
    {
        private readonly IApprovalRequestsRepository approvalRequestsRepo;

        public IEnumerable<ApprovalRequest> ApprovalRequests { get; private set; }
        public IEnumerable<SelectListItem> StatusSelectors { get; private set; }

        public string SearchValue { get; private set; }

        public IndexModel(IApprovalRequestsRepository approvalRequestsRepo)
        {
            this.approvalRequestsRepo = approvalRequestsRepo;
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
