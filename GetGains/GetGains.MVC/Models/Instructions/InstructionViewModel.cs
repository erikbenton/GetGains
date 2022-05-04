using GetGains.Core.Models.Instructions;
using GetGains.MVC.Mappers;
using GetGains.MVC.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.MVC.Models.Instructions;

public class InstructionViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ExerciseViewModel Exercise { get; set; }

    [Required]
    public int StepNumber { get; set; }

    [Required]
    public string Text { get; set; } = "";

    public InstructionViewModel(ExerciseViewModel exerciseViewModel)
    {
        Exercise = exerciseViewModel;
    }

    public InstructionViewModel(ExerciseViewModel exerciseViewModel, Instruction instruction)
    {
        Exercise = exerciseViewModel;

        Id = instruction.Id;
        Exercise = ExerciseMapper.Map(instruction.Exercise);
        StepNumber = instruction.StepNumber;
        Text = instruction.Text;
    }
}
