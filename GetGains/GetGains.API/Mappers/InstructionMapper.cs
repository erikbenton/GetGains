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
            IsNewEntry = model.Id == null,
            StepNumber = model.StepNumber,
            Text = model.Text,
        };
    }

    public static InstructionDto Map(Instruction instruction)
    {
        return new InstructionDto(instruction);
    }

    public static List<Instruction>? Map(List<InstructionDto>? instructions, Exercise referenceExercise)
    {
        if (instructions is null) return null;

        int stepNumber = 1;
        return instructions.Select(instruction =>
        {
            instruction.StepNumber = stepNumber++;
            return Map(instruction, referenceExercise);
        }).ToList();
    }

    public static void MapFromTo(InstructionDto model, Instruction instruction)
    {
        instruction.Text = model.Text;
    }

    public static void MapFromTo(List<InstructionDto>? models, List<Instruction>? instructions, Exercise referenceExercise)
    {
        if (models is null) return;

        int stepNumber = 1;
        var updatedInstructions = models.Select(model =>
        {
            model.StepNumber = stepNumber++;
            return Map(model, referenceExercise);
        }).ToList();
    }
}
