using OutOfOfficeWebApp.Models.Enums;
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
    }
}
