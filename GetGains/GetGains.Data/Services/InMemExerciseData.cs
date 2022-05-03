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
                Id = 1
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
        exerciseToUpdate.Author = exercise.Author;
        exerciseToUpdate.Instructions = exercise.Instructions;

        return true;
    }
}
