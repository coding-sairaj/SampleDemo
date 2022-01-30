using System;

namespace SampleDemo.Dtos;

public record ToDoDto {
    public Guid Id { get; init; }
    public string Task { get; init; }
    public bool IsComplete { get; init; }
    public DateTime CreatedDate { get; init; }    
    public DateTime? CompletedDate { get; init; }    
}