using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;

namespace GetGains.Data.Services;

public interface IExerciseData
{
    void AddExercise(Exercise exercise);

    bool Delete(Exercise exercise);

    Task<List<Exercise>> GetExercisesAsync(bool populateInstructions);

    Task<Exercise?> GetExerciseAsync(int id, bool populateInstructions);

    Task<bool> SaveChangesAsync();
}
