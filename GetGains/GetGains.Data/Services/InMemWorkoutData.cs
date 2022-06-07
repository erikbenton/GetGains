using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Core.Models.Workouts;
using Microsoft.EntityFrameworkCore;

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
        IndexSetNumbers(workout);

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

        if (populateSets)
        {
            workouts.ForEach(PopulateSets);
        }

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

        if (populateSets && workout is not null)
        {
            PopulateSets(workout);
        }

        return workout;
    }

    public bool Update(Workout workout)
    {
        throw new NotImplementedException();
    }

    private void IndexSetNumbers(Workout workout)
    {
        int groupNumber = 1;
        workout.ExerciseGroups?.ForEach(group =>
        {
            group.GroupNumber = groupNumber++;
            int setNumber = 1;
            group.Sets?.ForEach(set =>
            {
                set.SetNumber = setNumber++;
            });
        });
    }

    private void PopulateSets(Workout workout)
    {
        workout.ExerciseGroups = context.WorkoutSetGroups
            .Where(group => group.Workout.Id == workout.Id)
            .Include(group => group.Exercise)
            .OrderBy(group => group.GroupNumber)
            .ToList();

        workout.ExerciseGroups.ForEach(group =>
        {
            group.Sets = context.WorkoutSets
                .Where(set => set.WorkoutSetGroup.Id == set.Id)
                .OrderBy(set => set.SetNumber)
                .Include(set => set.Exercise)
                .ToList();
        });
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

            var chestWorkout = new Workout("Chest", ExerciseCategory.Barbell)
            {
                Description = "Focus on Chest and other pushing muscles."
            };
            chestWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(chestWorkout, benchPress),
                new WorkoutSetGroup(chestWorkout, pullUp),
                new WorkoutSetGroup(chestWorkout, overHeadPress)
            };

            var backWorkout = new Workout("Back", ExerciseCategory.Barbell)
            {
                Description = "Focus on Upper back and other pulling muscles."
            };
            backWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(backWorkout, bentOverRows),
                new WorkoutSetGroup(backWorkout, overHeadPress),
                new WorkoutSetGroup(backWorkout, dumbbellCurls),
            };

            var legsCoreWorkout = new Workout("Legs and Core", ExerciseCategory.Barbell)
            {
                Description = "Focus on Leg foundations and Core strength." 
            };
            legsCoreWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(legsCoreWorkout, squat),
                new WorkoutSetGroup(legsCoreWorkout, calfRaises),
                new WorkoutSetGroup(legsCoreWorkout, weightedCrunch),
            };

            var armsShoulderWorkout = new Workout("Arms and Shoulders", ExerciseCategory.Dumbbell)
            {
                Description = "Focus on the \"accessory muscles\" that support most upper movement."
            };
            armsShoulderWorkout.ExerciseGroups = new List<WorkoutSetGroup>()
            {
                new WorkoutSetGroup(armsShoulderWorkout, arnoldPress),
                new WorkoutSetGroup(armsShoulderWorkout, dumbbellCurls),
                new WorkoutSetGroup(armsShoulderWorkout, dip),
                new WorkoutSetGroup(armsShoulderWorkout, pullUp),
            };

            var bikingWorkout = new Workout("Biking", ExerciseCategory.IndoorCardio)
            {
                Description = "Convenient, low-impact, high resistance cardio."
            };
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
