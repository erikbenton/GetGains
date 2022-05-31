using GetGains.API.Dtos.Instructions;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;

namespace GetGains.API.Mappers;

public class InstructionMapper
{
    public static Instruction Map(InstructionDto model, Exercise referenceExercise)
    {
        return new Instruction(referenceExercise)
        {
            Id = model.Id == null ? 0 : model.Id.Value,
            StepNumber = model.StepNumber,
            Text = model.Text,
        };
    }

    public static InstructionDto Map(Instruction instruction)
    {
        return new InstructionDto(instruction);
    }
}
