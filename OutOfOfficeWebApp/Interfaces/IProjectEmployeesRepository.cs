using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IProjectEmployeesRepository
    {
        Task<IEnumerable<Project>> RelatedProjects(int employeeId);
        Task<IEnumerable<Employee>> RelatedEmployees(int projectId);

        Task Add(int projectId, int employeeId);
        void Remove(int projectId, int employeeId);
    }
}
