namespace OutOfOfficeWebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int SubdivisionId { get; set; }
        public int PositionId { get; set; }
        public int StatusId { get; set; }
        public int PeoplePartnerId { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public virtual byte[]? Photo { get; set; }
        public int RoleId { get; set; }
        public List<int> AssignedProjects { get; set; } = new List<int>();
    }
}
