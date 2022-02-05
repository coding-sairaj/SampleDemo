using SampleDemo.Models;

namespace SampleDemo.Repositories;


public interface ITodoTasksRepository
{
    Task<ToDo> GetTaskAsync(Guid id);
    Task<IEnumerable<ToDo>> GetTasksAsync();
    Task CreateTaskAsync(ToDo todo);
    Task UpdateTaskAsync(ToDo todo);
    Task DeleteTaskAsync(Guid id);
}