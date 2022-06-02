using GetGains.Core.Models.Workouts;

namespace GetGains.Data.Services;

public interface IWorkoutData
{
    List<Workout> GetAll();

    Workout? GetById(int id);

    Workout Add(Workout workout);

    bool Update(Workout workout);

    bool Delete(Workout workout);
}
