using System;
using System.ComponentModel.DataAnnotations;

namespace SampleDemo.Dtos;

public record CreateTodoDto {
    [Required]
    public string Task { get; init; }
}