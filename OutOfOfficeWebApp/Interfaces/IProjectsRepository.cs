using OutOfOfficeWebApp.Models;

namespace OutOfOfficeWebApp.Interfaces
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<Project>> All();
        Task<Project?> GetById(int id);
    }
}
