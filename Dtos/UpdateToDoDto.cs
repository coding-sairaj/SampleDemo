using System;
using System.ComponentModel.DataAnnotations;

namespace SampleDemo.Dtos;

public record UpdateToDoDto {
    [Required]
    public string Task { get; init; }
    public bool IsComplete { get; init; } 
}