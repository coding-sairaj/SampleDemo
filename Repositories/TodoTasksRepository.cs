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

    public IEnumerable<ToDo> GetTasks()
    {
        return tasks;
    }

    public ToDo GetTask(Guid id)
    {
        return tasks.Where(task => task.Id == id).SingleOrDefault();
    }

    public void CreateTask(ToDo todo)
    {
        tasks.Add(todo);
    }

    public void UpdateTask(ToDo todo)
    {
        var index = tasks.FindIndex(existingTask => existingTask.Id == todo.Id);
        tasks[index] = todo;
    }

    public void DeleteTask(Guid id)
    {
        var index = tasks.FindIndex(existingTask => existingTask.Id == id);
        tasks.RemoveAt(index);
    }
}