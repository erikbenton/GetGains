using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Core.Models.Workouts;

namespace GetGains.Data.Services;

public class InMemWorkoutData : IWorkoutData
{
    private readonly GainsDbContext context;

    public InMemWorkoutData(GainsDbContext context)
    {
        this.context = context;
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
        return GetAll(false);
    }

    public List<Workout> GetAll(bool populateSets = false)
    {
        var workouts = context.Workouts.OrderBy(x => x.Name).ToList();

        return workouts;
    }

    public Workout? GetById(int id)
    {
        return GetById(id, true);
    }

    public Workout? GetById(int id, bool populateSets = true)
    {
        var workout = context.Workouts
            .FirstOrDefault(workout => workout.Id == id);

        return workout;
    }

    public bool Update(Workout workout)
    {
        throw new NotImplementedException();
    }

    public static void SeedData(IWorkoutData workoutContext, List<Exercise> exercises)
    {
        try
        {
            var benchPress = exercises.First(exercise => exercise.Name == "Bench Press" && exercise.Category == ExerciseCategory.Barbell);
            var squat = exercises.First(exercise => exercise.Name == "Squat");
            var pullUp = exercises.First(exercise => exercise.Name == "Pull Up"); ;
            var overHeadPress = exercises.First(exercise => exercise.Name == "Over Head Press");
            var dumbbellCurls = exercises.First(exercise => exercise.Name == "Dumbbell Curls");
            var indoorBiking = exercises.First(exercise => exercise.Name == "Indoor Biking");
            var weightedCrunch = exercises.First(exercise => exercise.Name == "Weighted Crunch");
            var bentOverRows = exercises.First(exercise => exercise.Name == "Bent Over Rows");
            var lateralRaises = exercises.First(exercise => exercise.Name == "Lateral Raises");
            var calfRaises = exercises.First(exercise => exercise.Name == "Calf Raises");
            var benchPressDB = exercises.First(exercise => exercise.Name == "Bench Press" && exercise.Category == ExerciseCategory.Dumbbell);
            var arnoldPress = exercises.First(exercise => exercise.Name == "Arnold Press");
            var dip = exercises.First(exercise => exercise.Name == "Dip");

            var chestWorkout = new Workout("Chest", ExerciseCategory.Barbell);
            chestWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(chestWorkout, benchPress),
                new WorkoutSetGroup(chestWorkout, pullUp),
                new WorkoutSetGroup(chestWorkout, overHeadPress)
            };

            var backWorkout = new Workout("Back", ExerciseCategory.Barbell);
            backWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(backWorkout, bentOverRows),
                new WorkoutSetGroup(backWorkout, overHeadPress),
                new WorkoutSetGroup(backWorkout, dumbbellCurls),
            };

            var legsCoreWorkout = new Workout("Legs and Core", ExerciseCategory.Barbell);
            legsCoreWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(legsCoreWorkout, squat),
                new WorkoutSetGroup(legsCoreWorkout, calfRaises),
                new WorkoutSetGroup(legsCoreWorkout, weightedCrunch),
            };

            var armsShoulderWorkout = new Workout("Arms and Shoulders", ExerciseCategory.Dumbbell);
            armsShoulderWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(armsShoulderWorkout, arnoldPress),
                new WorkoutSetGroup(armsShoulderWorkout, dumbbellCurls),
                new WorkoutSetGroup(armsShoulderWorkout, dip),
                new WorkoutSetGroup(armsShoulderWorkout, pullUp),
            };

            var bikingWorkout = new Workout("Biking", ExerciseCategory.IndoorCardio);
            bikingWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(bikingWorkout, indoorBiking),
            };

            var workouts = new List<Workout>()
            {
                chestWorkout,
                backWorkout,
                legsCoreWorkout,
                armsShoulderWorkout,
                bikingWorkout
            };

            workouts.ForEach(workout =>
            {
                workoutContext.Add(workout);
            });
        }
        catch
        {
            return;
        }
    }
}
