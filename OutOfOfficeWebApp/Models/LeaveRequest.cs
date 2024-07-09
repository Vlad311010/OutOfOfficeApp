using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeWebApp.Models
{
    public class LeaveRequest
    {
        [Key]
        public int ID { get; set; }

        [Required, ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required, ForeignKey("AbsenceReason")]
        public int AbsenceReasonId { get; set; }
        public AbsenceReason AbsenceReason { get; set; } = default!;

        [Required, Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        [MaxLength(1000), Column(TypeName = "varchar")]
        public string? Comment { get; set; }

        [Required, ForeignKey("Status")]
        public int StatusId { get; set; }
        public RequestStatus Status { get; set; } = default!;

        public LeaveRequest() { }

        public LeaveRequest(LeaveRequestViewModel viewModel) 
        {
            EmployeeId = viewModel.EmployeeId;
            AbsenceReasonId = viewModel.AbsenceReasonId;
            StatusId = viewModel.StatusId;
            StartDate = viewModel.StartDate;
            EndDate = viewModel.EndDate;
            Comment = viewModel.Comment;
        }

        public int RequireDays()
        {
            return LeaveRequest.RequireDays(StartDate, EndDate);
        }

        public static int RequireDays(DateTime start, DateTime end)
        {
            TimeSpan difference = end - start;
            return difference.Days + 1;
        }
    }
}
