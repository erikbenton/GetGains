using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.MVC.Models.Exercises;
using GetGains.MVC.Models.Instructions;

namespace GetGains.MVC.Mappers;

public static class InstructionMapper
{
    public static Instruction Map(InstructionViewModel model, Exercise referenceExercise)
    {
        Instruction instruction = new(referenceExercise)
        {
            Id = model.Id,
            StepNumber = model.StepNumber,
            Text = model.Text,
        };

        return instruction;
    }

    public static InstructionViewModel Map(Instruction instruction, ExerciseViewModel referenceExercise)
    {
        InstructionViewModel model = new(referenceExercise)
        {
            Id = instruction.Id,
            StepNumber = instruction.StepNumber,
            Text = instruction.Text,
        };

        return model;
    }
}
