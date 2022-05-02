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
        throw new NotImplementedException();
    }

    public void Delete(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public ICollection<Exercise> GetAll()
    {
        throw new NotImplementedException();
    }

    public Exercise? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Exercise? Update(Exercise exercise)
    {
        throw new NotImplementedException();
    }
}
