namespace OutOfOfficeWebApp.ViewModels
{
    public class ProjectViewModel
    {
        public int ID { get; set; }
        public int ProjectTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public string? Comment { get; set; }
        public int StatusId { get; set; }
    }
}
