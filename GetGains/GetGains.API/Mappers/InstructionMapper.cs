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

    public static void MapFromTo(
        List<InstructionDto> modelInstructions,
        List<Instruction> exerciseInstructions,
        Exercise referenceExercise)
    {
        var (instructionIdsToDelete, instructionIdsToKeep) =
            BisectInstructionIds(exerciseInstructions, modelInstructions);

        if (instructionIdsToDelete is not null
            && instructionIdsToDelete.Count > 0)
        {
            RemoveDeletedInstructions(exerciseInstructions, instructionIdsToDelete);
        }

        UpdateChangedInstructions(modelInstructions, exerciseInstructions, instructionIdsToKeep);
        AddNewInstructions(modelInstructions, referenceExercise);
        IndexInstructionStepNumbers(exerciseInstructions);
    }

    /// <summary>
    /// Splits the Ids of all instructions involved into
    /// lists of Ids to Delete or to Keep.
    /// </summary>
    /// <param name="exerciseInstructions">Instructions belonging to the db exercise.</param>
    /// <param name="modelInstructions">Instructions belonging to the updated model.</param>
    /// <returns>Tuple of lists of Ids to Delete and to Keep.</returns>
    private static (List<int>, List<int>) BisectInstructionIds(
        List<Instruction> exerciseInstructions, List<InstructionDto> modelInstructions)
    {
        var savedInstructionIds = exerciseInstructions
            .Select(instr => instr.Id).ToList();

        var instructionIdsToKeep = modelInstructions
            .Select(instr =>
                instr.Id.HasValue ? instr.Id.Value : 0)
            .ToList();

        var instructionIdsToDelete = savedInstructionIds
            .Except(instructionIdsToKeep)
            .ToList();

        return (instructionIdsToDelete, instructionIdsToKeep);
    }

    private static void IndexInstructionStepNumbers(List<Instruction> exerciseInstructions)
    {
        var stepNumber = 1;
        exerciseInstructions.ForEach(instr =>
        {
            instr.StepNumber = stepNumber++;
        });
    }

    private static void AddNewInstructions(List<InstructionDto> modelInstructions, Exercise exercise)
    {
        modelInstructions.ForEach(instr =>
        {
            if (instr.Id is null)
            {
                exercise.Instructions?.Add(Map(instr, exercise));
            }
        });
    }

    private static void UpdateChangedInstructions(List<InstructionDto> modelInstructions, List<Instruction> exerciseInstructions, List<int> instructionIdsToKeep)
    {
        exerciseInstructions.ForEach(instr =>
        {
            if (instructionIdsToKeep.Contains(instr.Id))
            {
                var modelInstr = modelInstructions.First(i => i.Id == instr.Id);
                MapFromTo(modelInstr, instr);
            }
        });
    }

    private static void RemoveDeletedInstructions(List<Instruction> exerciseInstructions, List<int> instructionIdsToDelete)
    {
        var instructionsToDelete = new List<Instruction>();

        exerciseInstructions.ForEach(instr =>
        {
            if (instructionIdsToDelete.Contains(instr.Id))
            {
                instructionsToDelete.Add(instr);
            }
        });

        instructionsToDelete.ForEach(instr =>
        {
            exerciseInstructions.Remove(instr);
        });
    }
}
