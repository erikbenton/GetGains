using GetGains.Core.Models.Exercises;

namespace GetGains.Data.Services;

public class InMemExerciseData : IExerciseData
{
    private readonly List<Exercise> _exercises;

    public InMemExerciseData()
    {
        _exercises = new List<Exercise>()
        {
            new Exercise(),
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

    public ICollection<Exercise> GetAll()
    {
        return _exercises;
    }

    public Exercise? GetById(int id)
    {
        return _exercises.FirstOrDefault(e => e.Id == id);
    }

    public bool Update(Exercise exercise)
    {
        var exerciseToUpdate = _exercises.FirstOrDefault(e => e.Id == exercise.Id);

        if (exerciseToUpdate is null) return false;

        exerciseToUpdate = exercise;

        return true;
    }
}
