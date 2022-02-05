using System.Collections.Generic;
using System.Linq;
using SampleDemo.Models;
using System;

namespace SampleDemo.Repositories;

public class TodoTasksRepository : ITodoTasksRepository
{
    private readonly List<ToDo> tasks = new()
    {
        new ToDo { Id = Guid.NewGuid(), Task = "Task 1", IsComplete = true, CreatedDate = DateTime.Now, CompletedDate = DateTime.Now },
        new ToDo { Id = Guid.NewGuid(), Task = "Task 2", IsComplete = false, CreatedDate = DateTime.Now },
        new ToDo { Id = Guid.NewGuid(), Task = "Task 3", IsComplete = false, CreatedDate = DateTime.Now }
    };

    public async Task<IEnumerable<ToDo>> GetTasksAsync()
    {
        return await Task.FromResult(tasks);
    }

    public async Task<ToDo> GetTaskAsync(Guid id)
    {
        var task = tasks.Where(task => task.Id == id).SingleOrDefault();
        return await Task.FromResult(task);
    }

    public async Task CreateTaskAsync(ToDo todo)
    {
        tasks.Add(todo);
        await Task.CompletedTask;
    }

    public async Task UpdateTaskAsync(ToDo todo)
    {
        var index = tasks.FindIndex(existingTask => existingTask.Id == todo.Id);
        tasks[index] = todo;
        await Task.CompletedTask;
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var index = tasks.FindIndex(existingTask => existingTask.Id == id);
        tasks.RemoveAt(index);
        await Task.CompletedTask;
    }
}