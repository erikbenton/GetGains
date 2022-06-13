using GetGains.API.Dtos.Exercises;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Data.Services;

namespace GetGains.API.Mappers;

public class ExerciseMapper
{
    public static Exercise Map(ExerciseDto model)
    {
        Exercise exercise = new()
        {
            Id = model.Id == null ? 0 : model.Id.Value,
            Name = model.Name,
            BodyPart = model.BodyPart.GetBodyPart(),
            Category = model.Category.GetCategory(),
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Author = model.Author,
        };

        exercise.Instructions = model.Instructions?
            .Select(
                instructionModel => InstructionMapper.Map(instructionModel, exercise))
            .ToList();

        return exercise;
    }

    public static ExerciseDto Map(Exercise exercise, bool populateInstructions = false)
    {
        return new ExerciseDto(exercise, populateInstructions);
    }

    public static void MapFromTo(ExerciseDto model, Exercise exercise)
    {
        exercise.Name = model.Name;
        exercise.BodyPart = model.BodyPart.GetBodyPart();
        exercise.Category = model.Category.GetCategory();
        exercise.Description = model.Description;
        exercise.MediaUrl = model.MediaUrl;
        exercise.Author = model.Author;

        if (model.Instructions is null) return;

        var savedInstructionIds = exercise.Instructions?
            .Select(instr => instr.Id).ToList();

        var modelInstructionIds = model.Instructions
            .Select(instr =>
                instr.Id.HasValue ? instr.Id.Value : 0)
            .ToList();

        var instructionIdsToDelete = savedInstructionIds?
            .Except(modelInstructionIds)
            .ToList();

        var instructionsToDelete = new List<Instruction>();

        if (instructionIdsToDelete is not null)
        {
            exercise.Instructions?.ForEach(instr =>
            {
                if (instructionIdsToDelete.Contains(instr.Id))
                {
                    instructionsToDelete.Add(instr);
                }
                if (modelInstructionIds.Contains(instr.Id))
                {
                    var modelInstr = model.Instructions.First(i => i.Id == instr.Id);
                    InstructionMapper.MapFromTo(modelInstr, instr);
                }
            });
        }

        instructionsToDelete.ForEach(instr =>
        {
            exercise.Instructions?.Remove(instr);
        });

        model.Instructions.ForEach(instr =>
        {
            if (instr.Id is null)
            {
                exercise.Instructions?.Add(InstructionMapper.Map(instr, exercise));
            }
        });

        var stepNumber = 1;
        exercise.Instructions?.ForEach(instr =>
        {
            instr.StepNumber = stepNumber++;
        });
    }
}
