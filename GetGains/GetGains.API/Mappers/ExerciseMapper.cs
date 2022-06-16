using GetGains.API.Dtos.Exercises;
using GetGains.API.Dtos.Instructions;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Data.Services;

namespace GetGains.API.Mappers;

public class ExerciseMapper
{
    public static Exercise Map(ExerciseForCreationDto model)
    {
        Exercise exercise = new()
        {
            Id = 0,
            Name = model.Name,
            BodyPart = model.BodyPart.GetBodyPart(),
            Category = model.Category.GetCategory(),
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Author = model.Author,
        };

        exercise.Instructions = model.Instructions
            .Select(instructionModel =>
                InstructionMapper.Map(instructionModel, exercise))
            .ToList();

        return exercise;
    }

    public static Exercise Map(ExerciseForDeletionDto model)
    {
        Exercise exercise = new()
        {
            Id = model.Id,
            Name = model.Name,
            BodyPart = model.BodyPart.GetBodyPart(),
            Category = model.Category.GetCategory(),
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Author = model.Author,
        };

        exercise.Instructions = model.Instructions == null
            ? new List<Instruction>()
            : model.Instructions
                .Select(instructionModel =>
                    InstructionMapper.Map(instructionModel, exercise))
                .ToList();

        return exercise;
    }

    public static ExerciseDto Map(Exercise exercise, bool populateInstructions = false)
    {
        return new ExerciseDto(exercise, populateInstructions);
    }

    public static void MapFromTo(ExerciseForUpdatingDto model, Exercise exercise)
    {
        exercise.Name = model.Name;
        exercise.BodyPart = model.BodyPart.GetBodyPart();
        exercise.Category = model.Category.GetCategory();
        exercise.Description = model.Description;
        exercise.MediaUrl = model.MediaUrl;
        exercise.Author = model.Author;

        InstructionMapper.MapFromTo(model.Instructions, exercise.Instructions, exercise);
    }
}
