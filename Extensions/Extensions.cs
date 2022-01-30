using SampleDemo.Dtos;
using SampleDemo.Models;

namespace SampleDemo.Extensions;

public static class Extensions{
    public static ToDoDto AsDto(this ToDo todo)
    {
        return new ToDoDto {
            Id = todo.Id,
            Task  = todo.Task,
            IsComplete = todo.IsComplete,
            CreatedDate = todo.CreatedDate,
            CompletedDate = todo.CompletedDate
        };
    }
}