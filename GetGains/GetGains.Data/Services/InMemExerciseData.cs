using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;

namespace GetGains.Data.Services;

public class InMemExerciseData : IExerciseData
{
    private readonly List<Exercise> _exercises;

    public InMemExerciseData()
    {
        _exercises = new List<Exercise>()
        {
            new Exercise()
            {
                Id = 1,
                Name = "Bench Press",
                BodyPart = BodyPart.Chest,
                Category = ExerciseCategory.Barbell,
                Description = "Press the bar up from your chest",
                Instructions = null,
                Author = "Erik Benton"
            },
            new Exercise()
            {
                Id = 2,
                Name = "Squat",
                BodyPart = BodyPart.Legs,
                Category = ExerciseCategory.Barbell,
                Description = "Stand up with the barbell over your shoulders",
                Instructions = null,
                Author = "Kyle McNurmann"
            },
            new Exercise()
            {
                Id = 3,
                Name = "Pull Up",
                BodyPart = BodyPart.UpperBack,
                Category = ExerciseCategory.Bodyweight,
                Description = "Pull yourself up over the bar",
                Instructions = null,
                Author = "Alexa Stils"
            },
            new Exercise()
            {
                Id = 4,
                Name = "Over Head Press",
                BodyPart = BodyPart.Shoulders,
                Category = ExerciseCategory.Barbell,
                Description = "Press the bar up over your head",
                Instructions = null,
                Author = "Jeff Lovely"
            },
            new Exercise()
            {
                Id = 5,
                Name = "Dumbbell Curls",
                BodyPart = BodyPart.Biceps,
                Category = ExerciseCategory.Dumbbell,
                Description = "Curl the dumbbell up from your waist to your shoulder",
                Instructions = null,
                Author = "Mike Vincent"
            },
            new Exercise()
            {
                Id = 6,
                Name = "Indoor Biking",
                BodyPart = BodyPart.Legs,
                Category = ExerciseCategory.IndoorCardio,
                Description = "Bike on a bike trainer",
                Instructions = null,
                Author = "Sarah Bennet"
            },
        };
    }

    public Exercise Add(Exercise exercise)
    {
        exercise.Id = _exercises.Max(e => e.Id) + 1;

        _exercises.Add(exercise);

        return exercise;
    }

    public bool Delete(Exercise exercise)
    {
        var exerciseToRemove = _exercises.FirstOrDefault(e => e.Id == exercise.Id);

        if (exerciseToRemove is null) return false;

        return _exercises.Remove(exerciseToRemove);
    }

    public List<Exercise> GetAll()
    {
        return _exercises.OrderBy(e => e.Name).ToList();
    }

    public Exercise? GetById(int id)
    {
        return _exercises.FirstOrDefault(e => e.Id == id);
    }

    public bool Update(Exercise exercise)
    {
        var exerciseToUpdate = GetById(exercise.Id);

        if (exerciseToUpdate is null) return false;

        exerciseToUpdate.Name = exercise.Name;
        exerciseToUpdate.BodyPart = exercise.BodyPart;
        exerciseToUpdate.Category = exercise.Category;
        exerciseToUpdate.Description = exercise.Description;
        exerciseToUpdate.MediaUrl = exercise.MediaUrl;
        exerciseToUpdate.Author = exercise.Author;
        exerciseToUpdate.Instructions = exercise.Instructions;

        return true;
    }
}
