using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Lists.LeaveRequests
{
    public class AddModel : PageModel
    {
        private readonly ILeaveRequestsRepository leaveRepo;
        private readonly IApprovalRequestsRepository approveRepo;
        private readonly IEmployeesRepository employeeRepo;

        [BindProperty]
        public LeaveRequestViewModel LeaveRequest { get; set; }

        public IEnumerable<SelectListItem> AbsenceReasonSelector { get; private set; }

        public AddModel(ILeaveRequestsRepository leaveRepo, IApprovalRequestsRepository approveRepo, IEmployeesRepository employeeRepo)
        {
            this.leaveRepo = leaveRepo;
            this.approveRepo = approveRepo;
            this.employeeRepo = employeeRepo;
        }

        private async Task InitForm(Employee requester)
        {
            LeaveRequest = new LeaveRequestViewModel();
            LeaveRequest.EmployeeId = requester.ID;
            LeaveRequest.EmployeeName = requester.FullName;
            LeaveRequest.StatusId = (int)RequestStatusEnum.Submitted;
            LeaveRequest.StartDate = DateTime.Now;
            LeaveRequest.EndDate = DateTime.Now;

            AbsenceReasonSelector = AbsenceReason.GetSelectList(-1, true);
            
        }
        
        public async Task<ActionResult> OnGetAsync()
        {
            Employee? requester = await User.GetActiveEmployee(employeeRepo);
            if (requester == null)  
                return Unauthorized();

            await InitForm(requester);
            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            Employee? requester = await User.GetActiveEmployee(employeeRepo);
            if (requester == null)
                return Unauthorized();

            LeaveRequest.StatusId = (int)RequestStatusEnum.Submitted;
            int days = Models.LeaveRequest.RequireDays(LeaveRequest.StartDate, LeaveRequest.EndDate);
            bool validDates = LeaveRequest.EndDate >= LeaveRequest.StartDate;

            if (!validDates)
                ModelState.AddModelError("LeaveRequest.EndDate", "Invalid Time Span");
            else if (requester.OutOfOfficeBalance <= days)
                ModelState.AddModelError("LeaveRequest.EndDate", "Insufficient Out Of Office Balance");
            
            if (!TryValidateModel(LeaveRequest) || LeaveRequest == null)
            {
                await InitForm(requester);
                return Page();
            }


            int createdLeaveRequestId = leaveRepo.Add(LeaveRequest);
            await leaveRepo.Save();

            ApprovalRequest approvalRequests = new ApprovalRequest();
            approvalRequests.ApproverId = requester.PeoplePartnerId;
            approvalRequests.LeaveRequestId = createdLeaveRequestId;
            approvalRequests.StatusId = (int)RequestStatusEnum.New;

            approveRepo.Add(approvalRequests);
            await approveRepo.Save();


            return RedirectToPage("/Leave Requests/Index");
        }
            
    }
}
