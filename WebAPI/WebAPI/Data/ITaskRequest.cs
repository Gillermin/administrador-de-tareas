using WebAPI.Models;

namespace WebAPI.Data
{
    public interface ITaskRequest
    {
        Task<IEnumerable<Tasks>> GetAllTasks();
        Task<IEnumerable<Tasks>> GetTasksFiltred(Filter filter);
        Task<IEnumerable<Collaborator>> GetCollaborators();
        Task<Tasks> GetTask(int id);
        Task<bool> InsertTask(Tasks tasks);
        Task<bool> UpdateTask(Tasks tasks);
        Task<bool> DeleteTask(int id);
        
    }
}
