using GetGains.Core.Models.Exercises;
using GetGains.MVC.Models.Exercises;

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

        // TODO - Instructions will have their
        // own ViewModel in the future and will
        // need their own mapping protocol
        exercise.Instructions = model.Instructions;

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

        // TODO - Instructions will have their
        // own Model in the future and will
        // need their own mapping protocol
        model.Instructions = exercise.Instructions;

        return model;
    }
}
