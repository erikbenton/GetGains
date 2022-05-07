using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;

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
                MediaUrl = "https://images.unsplash.com/photo-1534368959876-26bf04f2c947?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80",
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
                MediaUrl = "https://images.unsplash.com/photo-1596357395217-80de13130e92?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1742&q=80",
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
                MediaUrl = "https://images.unsplash.com/photo-1597347316205-36f6c451902a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80",
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
                MediaUrl = "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80",
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

        int instructionId = 1;
        _exercises.ForEach(exercise =>
        {
            exercise.Instructions = new List<Instruction>()
            {
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    StepNumber = 1,
                    Text = "Warm up"
                },
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    StepNumber = 2,
                    Text = "Perform the exercise",
                },
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    StepNumber = 3,
                    Text = "Clean up the workout area",
                }
            };
        });
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
