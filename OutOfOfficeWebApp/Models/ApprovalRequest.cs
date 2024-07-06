using OutOfOfficeWebApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeWebApp.Models
{
    public class ApprovalRequest
    {
        [Key]
        public int ID { get; set; }

        [Required, ForeignKey("Approver")]
        public int ApproverId { get; set; }
        public Employee Approver { get; set; } = null!;

        [Required, ForeignKey("LeaveRequest")]
        public int LeaveRequestId { get; set; }
        public LeaveRequest LeaveRequest { get; set; } = null!;

        [Required, ForeignKey("Status")]
        public int StatusId { get; set; }
        public RequestStatus Status { get; set; } = default!;

        [MaxLength(1000), Column(TypeName = "varchar")]
        public string? Comment { get; set; }
    }
}
