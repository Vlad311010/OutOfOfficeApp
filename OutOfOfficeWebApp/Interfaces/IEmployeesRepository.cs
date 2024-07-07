using OutOfOfficeWebApp.Models;
using System.Linq.Expressions;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> All();
        Task<Employee?> GetById(int id);
        Task<IEnumerable<Employee>> Where(Expression<Func<Employee, bool>> predicate);
        Task<Employee?> Add(Employee user);
        Task<Employee?> Update(Employee user);
        Task<Employee?> Remove(int id);
        public Task Save();
    }
}
