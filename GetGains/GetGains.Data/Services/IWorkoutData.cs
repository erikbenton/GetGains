using GetGains.Core.Models.Workouts;

namespace GetGains.Data.Services;

public interface IWorkoutData
{
    List<Workout> GetAll();

    List<Workout> GetAll(bool populateSets = false);

    Workout? GetById(int id);

    Workout? GetById(int id, bool populateSets = true);

    Workout Add(Workout workout);

    bool Update(Workout workout);

    bool Delete(Workout workout);
}
