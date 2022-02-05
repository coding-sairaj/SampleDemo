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
[Route("[controller]")]
public class TodoController : ControllerBase {
    private readonly ITodoTasksRepository repository;

    public TodoController(ITodoTasksRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<ToDoDto>> GetTasksAsync()
    {
        var items = (await repository.GetTasksAsync())
                        .Select(todo => todo.AsDto());
        return items;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoDto>> GetTaskAsync(Guid id)
    {
        var item = await repository.GetTaskAsync(id);
        if(item is null)
            return NotFound();
        return item.AsDto();
    }
    [HttpPost]
    public async Task<ActionResult<ToDoDto>> CreateTaskAsync(CreateTodoDto toDo)
    {
        ToDo task = new() {
            Id = Guid.NewGuid(),
            Task = toDo.Task,
            CreatedDate = DateTime.Now
        };

        await repository.CreateTaskAsync(task);

        return CreatedAtAction(nameof(GetTaskAsync), new {id = task.Id}, task.AsDto());
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTaskAsync(Guid id, UpdateToDoDto todo)
    {
        var existingTask = await repository.GetTaskAsync(id);
        if(existingTask is null)
            return NotFound();
        ToDo updatedTask = existingTask with {
            Task = todo.Task,
            IsComplete = todo.IsComplete,
            CompletedDate = todo.IsComplete ? DateTime.Now : null
        };

        await repository.UpdateTaskAsync(updatedTask);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var existingTask = await repository.GetTaskAsync(id);
        if(existingTask is null)
            return NotFound();
        await repository.DeleteTaskAsync(id);
        return NoContent();
    }
}