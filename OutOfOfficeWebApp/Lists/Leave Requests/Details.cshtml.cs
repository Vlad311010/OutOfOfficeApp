using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Lists.LeaveRequests
{
    public class DetailsModel : PageModel
    {
        private readonly ILeaveRequestsRepository leaveRepo;
        private readonly IApprovalRequestsRepository approveRepo;
        private readonly IAuthorizationService authorizationService;

        public LeaveRequest LeaveRequest { get; private set; }

        public int ApprovalRequestId { get; private set; } = 0;


        public DetailsModel(ILeaveRequestsRepository leaveRepo, IApprovalRequestsRepository approveRepo, IAuthorizationService authorizationService)
        {
            this.leaveRepo = leaveRepo;
            this.approveRepo = approveRepo;
            this.authorizationService = authorizationService;
        }

        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            LeaveRequest = await leaveRepo.GetById(id);
            if (LeaveRequest == null)
                return NotFound();

            var resource = new OwnedPageAuthorizationRequest { AllowedUsers = new List<int>() { LeaveRequest.EmployeeId } };
            var authorizationResult = await authorizationService.AuthorizeAsync(User, resource, "OwnerOrManager");
            if (!authorizationResult.Succeeded)
                return Forbid();


            ApprovalRequestId = (await approveRepo.GetByLeaveRequest(LeaveRequest.ID)).ID;

            return Page();
        }

        
            
    }
}
