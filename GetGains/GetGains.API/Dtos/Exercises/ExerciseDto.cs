using GetGains.API.Dtos.Instructions;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GetGains.API.Dtos.Exercises;

public class ExerciseDto
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

    public List<InstructionDto>? Instructions { get; set; }

    public string? Author { get; set; }

    [JsonConstructor]
    public ExerciseDto(int id, string name, string category, string bodyPart)
    {
        Id = id;
        Name = name;
        Category = category;
        BodyPart = bodyPart;
    }

    public ExerciseDto(Exercise exercise, bool populateInstructions = false)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = exercise.Category.GetLabel();
        BodyPart = exercise.BodyPart.GetLabel();
        Description = exercise.Description;
        MediaUrl = exercise.MediaUrl;
        Author = exercise.Author;

        if (populateInstructions)
        {
            Instructions = exercise.Instructions
                .Select(
                    instruction => new InstructionDto(instruction))
                .ToList();
        }
    }
}
