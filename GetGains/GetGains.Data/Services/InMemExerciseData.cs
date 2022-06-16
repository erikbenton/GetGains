using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace GetGains.Data.Services;

public class InMemExerciseData : IExerciseData
{
    private readonly GainsDbContext context;

    public InMemExerciseData(GainsDbContext context)
    {
        this.context = context;
    }

    public void AddExercise(Exercise exercise)
    {
        context.Exercises.Add(exercise);
    }

    public async Task<List<Exercise>> GetExercisesAsync(bool populateInstructions)
    {
        var exercises = populateInstructions
            ? await context.Exercises
                .OrderBy(exer => exer.Name)
                .Include(exer => exer.Instructions.OrderBy(instr => instr.StepNumber))
                .ToListAsync()
            : await context.Exercises
                .OrderBy(exer => exer.Name)
                .ToListAsync();

        if (populateInstructions)
        {
            exercises.ForEach(exer =>
            {
                exer.Instructions = exer.Instructions
                    .OrderBy(instr => instr.StepNumber).ToList();
            });
        }

        return exercises;
    }

    public async Task<Exercise?> GetExerciseAsync(int id, bool populateInstructions)
    {
        IQueryable<Exercise> query = populateInstructions
            ? context.Exercises
                .Include(e => e.Instructions)
            : context.Exercises;

        var exercise = await query
            .Where(e => e.Id == id).FirstOrDefaultAsync();

        if (exercise is null) return null;

        exercise.Instructions = exercise.Instructions
            .OrderBy(instr => instr.StepNumber).ToList();

        return exercise;
    }

    public async Task<bool> SaveChangesAsync()
    {
        var saved = (await context.SaveChangesAsync() >= 0);

        if (saved) context.ChangeTracker.Clear();

        return saved;
    }

    public bool Delete(Exercise exercise)
    {
        context.Exercises.Remove(exercise);

        return true;
    }

    public List<EntityEntry> CheckedChangedEntities()
    {
        var changedEntriesCopy = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Detached ||
                        e.State == EntityState.Unchanged ||
                        e.State == EntityState.Deleted)
            .ToList();

        return changedEntriesCopy;
    }

    public async static Task SeedData(IExerciseData context)
    {
        var benchPress = new Exercise()
        {
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up from your chest",
            MediaUrl = "https://images.unsplash.com/photo-1534368959876-26bf04f2c947",
            Author = "Erik Benton"
        };
        var squat = new Exercise()
        {
            Name = "Squat",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.Barbell,
            Description = "Stand up with the barbell over your shoulders",
            MediaUrl = "https://images.unsplash.com/photo-1596357395217-80de13130e92",
            Author = "Kyle McNurmann"
        };
        var pullUp = new Exercise()
        {
            Name = "Pull Up",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Bodyweight,
            Description = "Pull yourself up over the bar",
            MediaUrl = "https://images.unsplash.com/photo-1597347316205-36f6c451902a",
            Author = "Alexa Stils"
        };
        var overHeadPress = new Exercise()
        {
            Name = "Over Head Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up over your head",
            MediaUrl = "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3",
            Author = "Jeff Lovely"
        };
        var dumbbellCurls = new Exercise()
        {
            Name = "Dumbbell Curls",
            BodyPart = BodyPart.Biceps,
            Category = ExerciseCategory.Dumbbell,
            Description = "Curl the dumbbell up from your waist to your shoulder",
            Author = "Mike Vincent"
        };
        var indoorBiking = new Exercise()
        {
            Name = "Indoor Biking",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.IndoorCardio,
            Description = "Bike on a bike trainer",
            Author = "Sarah Bennet"
        };
        var weightedCrunch = new Exercise()
        {
            Name = "Weighted Crunch",
            BodyPart = BodyPart.Core,
            Category = ExerciseCategory.Dumbbell,
            Description = "Crunches with heavy dumbbell or plate",
            Author = "Josh Sylar"
        };
        var bentOverRows = new Exercise()
        {
            Name = "Bent Over Rows",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Barbell,
            Description = "Bend over and row the barbell",
            Author = "Nathan Trant"
        };
        var lateralRaises = new Exercise()
        {
            Name = "Lateral Raises",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Raise fully extended arms with dumbbells in them",
            Author = "Tasha Heffle"
        };
        var calfRaises = new Exercise()
        {
            Name = "Calf Raises",
            BodyPart = BodyPart.Calves,
            Category = ExerciseCategory.Barbell,
            Description = "Hold barbell in hands while standing on tippy-toes",
            Author = "Becky Yeung"
        };
        var benchPressDB = new Exercise()
        {
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Dumbbell,
            Description = "Bench press with dumbbells",
            Author = "Charles Orbeiter"
        };
        var arnoldPress = new Exercise()
        {
            Name = "Arnold Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Dumbbell shoulder press with full shoulder rotation"
        };
        var dip = new Exercise()
        {
            Name = "Dip",
            BodyPart = BodyPart.Triceps,
            Category = ExerciseCategory.Bodyweight,
            Description = "Hang so that you can slowly lower/dip your body using your arms/triceps"
        };
        var exercises = new List<Exercise>()
        {
            benchPress,
            squat,
            pullUp,
            overHeadPress,
            dumbbellCurls,
            indoorBiking,
            weightedCrunch,
            bentOverRows,
            lateralRaises,
            calfRaises,
            benchPressDB,
            arnoldPress,
            dip,
        };

        exercises.ForEach(exercise =>
        {
            exercise.Instructions = new List<Instruction>()
            {
                new Instruction(exercise)
                {
                    StepNumber = 1,
                    Text = "Warm up",
                    IsNewEntry = true,
                },
                new Instruction(exercise)
                {
                    StepNumber = 2,
                    Text = "Perform the exercise",
                    IsNewEntry = true,
                },
                new Instruction(exercise)
                {
                    StepNumber = 3,
                    Text = "Clean up the workout area",
                    IsNewEntry = true,
                }
            };
        });

        // Add them all to the context for now
        exercises.ForEach(exercise => context.AddExercise(exercise));

        await context.SaveChangesAsync();


    }
}

