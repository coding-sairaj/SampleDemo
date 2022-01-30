using System;
using System.Collections.Generic;
using SampleDemo.Models;

namespace SampleDemo.Repositories;


public interface ITodoTasksRepository
{
    ToDo GetTask(Guid id);
    IEnumerable<ToDo> GetTasks();
    void CreateTask(ToDo todo);
    void UpdateTask(ToDo todo);
    void DeleteTask(Guid id);
}