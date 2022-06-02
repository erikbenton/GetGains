using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Instructions;

public class Instruction
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Exercise Exercise { get; private set; }

    [Required]
    public int StepNumber { get; set; }

    [Required]
    public string Text { get; set; } = "";

    public Instruction()
    {

    }

    public Instruction(Exercise exercise)
    {
        Exercise = exercise;
    }
}
