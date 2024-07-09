using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutOfOfficeWebApp.Lists.ApprovalRequests
{
    public class DetailsModel : PageModel
    {
        private readonly ILeaveRequestsRepository leaveRepo;
        private readonly IApprovalRequestsRepository approveRepo;
        private readonly IEmployeesRepository employeeRepo;

        [BindProperty] public bool IsApproved { get; set; }

        [BindProperty, MaxLength(1000)] public string Comment { get; set; }

        public ApprovalRequest ApprovalRequest { get; private set; }

        public DetailsModel(ILeaveRequestsRepository leaveRepo, IApprovalRequestsRepository approveRepo, IEmployeesRepository employeeRepo)
        {
            this.leaveRepo = leaveRepo;
            this.approveRepo = approveRepo;
            this.employeeRepo = employeeRepo;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ApprovalRequest = await approveRepo.GetById(id);
            Comment = ApprovalRequest.Comment ?? string.Empty;
            if (ApprovalRequest == null)
                return NotFound();

            return Page();
        }

        public async Task<ActionResult> OnPostAsync(int id, bool IsApproved, string Comment)
        {
            if (IsApproved)
                return await Approve(id, Comment);
            else
                return await Reject(id, Comment);
        }

        private async Task<ActionResult> Approve(int id, string Comment) 
        {
            ApprovalRequest = await approveRepo.GetById(id);
            if (ApprovalRequest == null)
                return NotFound();

            ApprovalRequest.Comment = Comment;
            ApprovalRequest.StatusId = (int)RequestStatusEnum.Approved;
            LeaveRequest leaveRequest = await leaveRepo.GetById(ApprovalRequest.LeaveRequestId);
            leaveRequest.StatusId = ApprovalRequest.StatusId;
            Employee employee = await employeeRepo.GetById(leaveRequest.EmployeeId);
            int offDays = leaveRequest.RequireDays();
            if (employee.OutOfOfficeBalance < offDays)
            {
                ModelState.AddModelError("OutOfOfficeBalanceError", "Insufficient OutOfOfficeBalanceError");
                return Page();
            }
            employee.OutOfOfficeBalance -= offDays;


            await approveRepo.Save();

            return RedirectToPage();
        }

        public async Task<ActionResult> Reject(int id, string Comment)
        {
            ApprovalRequest = await approveRepo.GetById(id);
            if (ApprovalRequest == null)
                return NotFound();

            ApprovalRequest.Comment = Comment;
            ApprovalRequest.StatusId = (int)RequestStatusEnum.Rejected;
            LeaveRequest leaveRequest = await leaveRepo.GetById(ApprovalRequest.LeaveRequestId);
            leaveRequest.StatusId = ApprovalRequest.StatusId;
            await approveRepo.Save();


            return RedirectToPage();
        }
    }
}
