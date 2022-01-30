using System;

namespace SampleDemo.Models;

public record ToDo {
    public Guid Id { get; init; }
    public string Task { get; init; }
    public bool IsComplete { get; init; }
    public DateTime CreatedDate { get; init; }    
    public DateTime? CompletedDate { get; init; }    
}