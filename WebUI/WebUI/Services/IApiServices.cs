using WebUI.Models;

namespace WebUI.Services
{
    public interface IApiServices
    {
        public List<string> GetTasksFiltred(Tasks tasks);
        public List<string> GetTask(int id);
        public List<string> GetAllTasks();
        public List<string> InsertTask(Tasks tasks);
        public List<string> EditTask(Tasks tasks);
        public List<string> DeleteTask(int id);
        public List<string> getCollaborators();
        public string createJsonFilter(Tasks tasks);
        public string createJsonInsert(Tasks tasks);
    }
}
