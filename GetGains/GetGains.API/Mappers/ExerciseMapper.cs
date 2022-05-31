using GetGains.API.Dtos.Exercises;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;

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
}
