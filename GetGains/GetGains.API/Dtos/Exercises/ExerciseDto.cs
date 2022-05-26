using GetGains.API.Dtos.Instructions;
using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Exercises;

public class ExerciseDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ExerciseCategory Category { get; set; }

    [Required]
    [Display(Name = "Body Part")]
    public BodyPart BodyPart { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Media URL")]
    public string? MediaUrl { get; set; }

    public List<InstructionDto>? Instructions { get; set; }

    public string? Author { get; set; }

    public ExerciseDto(Exercise exercise, bool populateInstructions = false)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = exercise.Category;
        BodyPart = exercise.BodyPart;
        Description = exercise.Description;
        MediaUrl = exercise.MediaUrl;
        Author = exercise.Author;

        if (populateInstructions)
        {
            Instructions = exercise.Instructions?
                .Select(
                    instruction => new InstructionDto(instruction))
                .ToList();
        }
    }
}
