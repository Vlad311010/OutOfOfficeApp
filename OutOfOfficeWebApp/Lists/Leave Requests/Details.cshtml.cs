using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;

namespace OutOfOfficeWebApp.Lists.LeaveRequests
{
    public class DetailsModel : PageModel
    {
        private readonly ILeaveRequestsRepository leaveRepo;
        private readonly IApprovalRequestsRepository approveRepo;

        public LeaveRequest LeaveRequest { get; private set; }

        public int ApprovalRequestId { get; private set; } = 0;


        public DetailsModel(ILeaveRequestsRepository leaveRepo, IApprovalRequestsRepository approveRepo)
        {
            this.leaveRepo = leaveRepo;
            this.approveRepo = approveRepo;
        }

        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            LeaveRequest = await leaveRepo.GetById(id);
            if (LeaveRequest == null)
                return NotFound();

            // employee only
            ApprovalRequestId = (await approveRepo.GetByLeaveRequest(LeaveRequest.ID)).ID;

            return Page();
        }

        
            
    }
}
