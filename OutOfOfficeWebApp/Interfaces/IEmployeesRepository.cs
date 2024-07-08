using OutOfOfficeWebApp.Models;
using OutOfOfficeWebApp.ViewModels;
using System.Linq.Expressions;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> All();
        Task<Employee?> GetById(int id);
        Task<IEnumerable<Employee>> FindByName(string name);
        Task<IEnumerable<Employee>> Where(Expression<Func<Employee, bool>> predicate);
        void Add(EmployeeViewModel user);
        public Task Save();
    }
}
