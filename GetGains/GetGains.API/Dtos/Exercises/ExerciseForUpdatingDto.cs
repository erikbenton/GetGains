﻿using GetGains.API.Dtos.Instructions;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GetGains.API.Dtos.Exercises;

public class ExerciseForUpdatingDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string BodyPart { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public string? MediaUrl { get; set; }

    [Required]
    public List<InstructionDto> Instructions { get; set; }

    public string? Author { get; set; }

    [JsonConstructor]
    public ExerciseForUpdatingDto(string name, string category, string bodyPart, List<InstructionDto> instructions)
    {
        Name = name;
        Category = category;
        BodyPart = bodyPart;
        Instructions = instructions;
    }

    public ExerciseForUpdatingDto(Exercise exercise)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = exercise.Category.GetLabel();
        BodyPart = exercise.BodyPart.GetLabel();
        Description = exercise.Description;
        MediaUrl = exercise.MediaUrl;
        Author = exercise.Author;

        Instructions = exercise.Instructions
                .Select(instruction => new InstructionDto(instruction))
                .ToList();
    }
}
