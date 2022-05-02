using GetGains.Core.Models.Exercises;

namespace GetGains.Data.Services;

public interface IExerciseData
{
    ICollection<Exercise> GetAll();

    Exercise? GetById(int id);

    Exercise Add(Exercise exercise);

    Exercise? Update(Exercise exercise);

    void Delete(Exercise exercise);
}
