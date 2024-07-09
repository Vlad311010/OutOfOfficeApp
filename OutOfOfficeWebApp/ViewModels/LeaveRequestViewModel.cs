using OutOfOfficeWebApp.Models.Enums;
using OutOfOfficeWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutOfOfficeWebApp.ViewModels
{
    public class LeaveRequestViewModel
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public int AbsenceReasonId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}
