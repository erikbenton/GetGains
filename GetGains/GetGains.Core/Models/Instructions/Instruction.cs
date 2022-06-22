using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Instructions;

public class Instruction
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Exercise Exercise
    {
        get => _exercise
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Exercise));
        set => _exercise = value;
    }

    private Exercise? _exercise;

    public int ExerciseId { get; set; }

    [Required]
    public int StepNumber { get; set; }

    [Required]
    public string Text { get; set; } = "";

    public bool IsNewEntry { get; set; } = false;

    public Instruction()
    {

    }

    public Instruction(Exercise exercise)
    {
        Exercise = exercise;
        ExerciseId = exercise.Id;
    }
}
