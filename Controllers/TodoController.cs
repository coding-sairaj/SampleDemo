using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleDemo.Models;
using SampleDemo.Repositories;
using System.Linq;
using SampleDemo.Dtos;
using SampleDemo.Extensions;

namespace SampleDemo.Controllers;

[ApiController]
[Route("items")]
public class TodoController : ControllerBase {
    private readonly ITodoTasksRepository repository;

    public TodoController(ITodoTasksRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IEnumerable<ToDoDto> GetTasks()
    {
        var items = repository.GetTasks().Select(todo => todo.AsDto());
        return items;
    }
    [HttpGet("{id}")]
    public ActionResult<ToDoDto> GetTask(Guid id)
    {
        var item = repository.GetTask(id);
        if(item is null)
            return NotFound();
        return item.AsDto();
    }
    [HttpPost]
    public ActionResult<ToDoDto> CreateTask(CreateTodoDto toDo)
    {
        ToDo task = new() {
            Id = Guid.NewGuid(),
            Task = toDo.Task,
            CreatedDate = DateTime.Now
        };

        repository.CreateTask(task);

        return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task.AsDto());
    }
    [HttpPut("{id}")]
    public ActionResult UpdateTask(Guid id, UpdateToDoDto todo)
    {
        var existingTask = repository.GetTask(id);
        if(existingTask is null)
            return NotFound();
        ToDo updatedTask = existingTask with {
            Task = todo.Task,
            IsComplete = todo.IsComplete,
            CompletedDate = todo.IsComplete ? DateTime.Now : null
        };

        repository.UpdateTask(updatedTask);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        var existingTask = repository.GetTask(id);
        if(existingTask is null)
            return NotFound();
        repository.DeleteTask(id);
        return NoContent();
    }
}