using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> All();
        Task<Employee?> GetById(int id);
        Task<Employee?> Add(Employee user);
        Task<Employee?> Update(Employee user);
        Task<Employee?> Remove(int id);
    }
}
