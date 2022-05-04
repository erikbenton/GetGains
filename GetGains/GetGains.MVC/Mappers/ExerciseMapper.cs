using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.MVC.Models.Exercises;
using GetGains.MVC.Models.Instructions;

namespace GetGains.MVC.Mappers;

public static class ExerciseMapper
{
    public static Exercise Map(ExerciseViewModel model)
    {
        Exercise exercise = new()
        {
            Id = model.Id,
            Name = model.Name,
            BodyPart = model.BodyPart,
            Category = model.Category,
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Author = model.Author,
        };

        exercise.Instructions = model.Instructions?
            .Select(
                instructionModel => new Instruction(exercise))
            .ToList();

        return exercise;
    }

    public static ExerciseViewModel Map(Exercise exercise)
    {
        ExerciseViewModel model = new()
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Category = exercise.Category,
            BodyPart = exercise.BodyPart,
            Description = exercise.Description,
            MediaUrl = exercise.MediaUrl,
            Author = exercise.Author,
        };

        model.Instructions = exercise.Instructions?
            .Select(instruction => new InstructionViewModel(model))
            .ToList();

        return model;
    }
}
