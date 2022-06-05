using GetGains.Core.Models.Workouts;

namespace GetGains.Data.Services;

public static class InMemWorkoutState
{
    public static bool HasNoData = false;
}

public class InMemWorkoutData : IWorkoutData
{
    private readonly GainsDbContext context;

    public InMemWorkoutData(GainsDbContext context)
    {
        this.context = context;

        if (InMemWorkoutState.HasNoData)
        {
            // TODO Seed workouts
        }
    }

    public Workout Add(Workout workout)
    {
        context.Workouts.Add(workout);
        context.SaveChanges();
        return workout;
    }

    public bool Delete(Workout workout)
    {
        context.Workouts.Remove(workout);
        context.SaveChanges();
        return true;
    }

    public List<Workout> GetAll()
    {
        throw new NotImplementedException();
    }

    public Workout? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(Workout workout)
    {
        throw new NotImplementedException();
    }
}
