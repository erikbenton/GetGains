using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Core.Models.Templates;
using GetGains.Core.Models.Workouts;
using Microsoft.EntityFrameworkCore;

namespace GetGains.Data.Services;

public class GainsDbContext : DbContext
{
    public GainsDbContext(DbContextOptions<GainsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TemplateSet>()
            .HasOne(t => t.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<WorkoutSet>()
            .HasOne(w => w.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TemplateSetGroup>()
            .HasOne(t => t.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<WorkoutSetGroup>()
            .HasOne(w => w.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        //SeedData(modelBuilder);
    }

    public DbSet<Workout> Workouts => Set<Workout>();

    public DbSet<WorkoutSetGroup> WorkoutSetGroups => Set<WorkoutSetGroup>();

    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();

    public DbSet<Template> Templates => Set<Template>();

    public DbSet<TemplateSetGroup> TemplateSetGroups => Set<TemplateSetGroup>();

    public DbSet<TemplateSet> TemplateSets => Set<TemplateSet>();

    public DbSet<Exercise> Exercises => Set<Exercise>();

    public DbSet<Instruction> Instructions => Set<Instruction>();

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var benchPress = new Exercise()
        {
            Id = 1,
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up from your chest",
            MediaUrl = "https://images.unsplash.com/photo-1534368959876-26bf04f2c947",
            Author = "Erik Benton"
        };
        var squat = new Exercise()
        {
            Id = 2,
            Name = "Squat",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.Barbell,
            Description = "Stand up with the barbell over your shoulders",
            MediaUrl = "https://images.unsplash.com/photo-1596357395217-80de13130e92",
            Author = "Kyle McNurmann"
        };
        var pullUp = new Exercise()
        {
            Id = 3,
            Name = "Pull Up",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Bodyweight,
            Description = "Pull yourself up over the bar",
            MediaUrl = "https://images.unsplash.com/photo-1597347316205-36f6c451902a",
            Author = "Alexa Stils"
        };
        var overHeadPress = new Exercise()
        {
            Id = 4,
            Name = "Over Head Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up over your head",
            MediaUrl = "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3",
            Author = "Jeff Lovely"
        };
        var dumbbellCurls = new Exercise()
        {
            Id = 5,
            Name = "Dumbbell Curls",
            BodyPart = BodyPart.Biceps,
            Category = ExerciseCategory.Dumbbell,
            Description = "Curl the dumbbell up from your waist to your shoulder",
            Author = "Mike Vincent"
        };
        var indoorBiking = new Exercise()
        {
            Id = 6,
            Name = "Indoor Biking",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.IndoorCardio,
            Description = "Bike on a bike trainer",
            Author = "Sarah Bennet"
        };
        var weightedCrunch = new Exercise()
        {
            Id = 7,
            Name = "Weighted Crunch",
            BodyPart = BodyPart.Core,
            Category = ExerciseCategory.Dumbbell,
            Description = "Crunches with heavy dumbbell or plate",
            Author = "Josh Sylar"
        };
        var bentOverRows = new Exercise()
        {
            Id = 8,
            Name = "Bent Over Rows",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Barbell,
            Description = "Bend over and row the barbell",
            Author = "Nathan Trant"
        };
        var lateralRaises = new Exercise()
        {
            Id = 9,
            Name = "Lateral Raises",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Raise fully extended arms with dumbbells in them",
            Author = "Tasha Heffle"
        };
        var calfRaises = new Exercise()
        {
            Id = 10,
            Name = "Calf Raises",
            BodyPart = BodyPart.Calves,
            Category = ExerciseCategory.Barbell,
            Description = "Hold barbell in hands while standing on tippy-toes",
            Author = "Becky Yeung"
        };
        var benchPressDB = new Exercise()
        {
            Id = 11,
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Dumbbell,
            Description = "Bench press with dumbbells",
            Author = "Charles Orbeiter"
        };
        var arnoldPress = new Exercise()
        {
            Id = 12,
            Name = "Arnold Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Dumbbell shoulder press with full shoulder rotation"
        };
        var dip = new Exercise()
        {
            Id = 13,
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

        int instructionId = 1;
        exercises.ForEach(exercise =>
        {
            var instructions = new List<Instruction>()
            {
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    ExerciseId = exercise.Id,
                    StepNumber = 1,
                    Text = "Warm up",
                    IsNewEntry = true,
                },
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    ExerciseId = exercise.Id,
                    StepNumber = 2,
                    Text = "Perform the exercise",
                    IsNewEntry = true,
                },
                new Instruction(exercise)
                {
                    Id = instructionId++,
                    ExerciseId = exercise.Id,
                    StepNumber = 3,
                    Text = "Clean up the workout area",
                    IsNewEntry = true,
                }
            };

            modelBuilder.Entity<Instruction>().HasData(instructions);

            exercise.Instructions = instructions;
        });

        modelBuilder.Entity<Exercise>().HasData(exercises);

        int templateId = 1;
        var chestTemplate = new Template("Chest", ExerciseCategory.Barbell)
        {
            Id = templateId++,
            Description = "Focus on Chest and other pushing muscles."
        };

        int templateSetGroupId = 1;
        chestTemplate.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(chestTemplate, benchPress) { Id = templateSetGroupId++ },
            new TemplateSetGroup(chestTemplate, pullUp) { Id = templateSetGroupId++ },
            new TemplateSetGroup(chestTemplate, overHeadPress) { Id = templateSetGroupId++ }
        };

        int templateSetId = 1;
        var benchPressSet = chestTemplate.GroupTemplates[0];
        benchPressSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 1,
                TargetReps = 10,
                TargetWeight = 45
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 2,
                TargetReps = 8,
                TargetWeight = 95
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 3,
                TargetReps = 6,
                TargetWeight = 115
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 4,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 5,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 6,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                Id = templateSetId++,
                SetNumber = 7,
                TargetReps = 8,
                TargetWeight = 165
            },
        };

        var pullupSet = chestTemplate.GroupTemplates[1];
        pullupSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(pullupSet)
            {
                Id = templateSetId++,
                SetNumber = 1,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                Id = templateSetId++,
                SetNumber = 2,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                Id = templateSetId++,
                SetNumber = 3,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                Id = templateSetId++,
                SetNumber = 4,
                TargetReps = 10
            },
        };

        var overheadPressSet = chestTemplate.GroupTemplates[2];
        overheadPressSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(overheadPressSet)
            {
                Id = templateSetId++,
                SetNumber = 1,
                TargetReps = 8,
                TargetWeight = 65
            },
            new TemplateSet(overheadPressSet)
            {
                Id = templateSetId++,
                SetNumber = 2,
                TargetReps = 8,
                TargetWeight = 95
            },
            new TemplateSet(overheadPressSet)
            {
                Id = templateSetId++,
                SetNumber = 3,
                TargetReps = 6,
                TargetWeight = 95
            },
            new TemplateSet(overheadPressSet)
            {
                Id = templateSetId++,
                SetNumber = 4,
                TargetReps = 6,
                TargetWeight = 95
            }
        };

        var backTemplate = new Template("Back", ExerciseCategory.Barbell)
        {
            Id = templateId++,
            Description = "Focus on Upper back and other pulling muscles."
        };
        backTemplate.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(backTemplate, bentOverRows) { Id = templateSetGroupId++ },
            new TemplateSetGroup(backTemplate, overHeadPress) { Id = templateSetGroupId++ },
            new TemplateSetGroup(backTemplate, dumbbellCurls) { Id = templateSetGroupId++ },
        };

        var legsCoreTemplate = new Template("Legs and Core", ExerciseCategory.Barbell)
        {
            Id = templateId++,
            Description = "Focus on Leg foundations and Core strength."
        };
        legsCoreTemplate.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(legsCoreTemplate, squat) { Id = templateSetGroupId++ },
            new TemplateSetGroup(legsCoreTemplate, calfRaises) { Id = templateSetGroupId++ },
            new TemplateSetGroup(legsCoreTemplate, weightedCrunch) { Id = templateSetGroupId++ },
        };

        var armsShoulderTemplate = new Template("Arms and Shoulders", ExerciseCategory.Dumbbell)
        {
            Id = templateId++,
            Description = "Focus on the \"accessory muscles\" that support most upper movement."
        };
        armsShoulderTemplate.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(armsShoulderTemplate, arnoldPress) { Id = templateSetGroupId++ },
            new TemplateSetGroup(armsShoulderTemplate, dumbbellCurls) { Id = templateSetGroupId++ },
            new TemplateSetGroup(armsShoulderTemplate, dip) { Id = templateSetGroupId++ },
            new TemplateSetGroup(armsShoulderTemplate, pullUp) { Id = templateSetGroupId++ },
        };

        var bikingTemplate = new Template("Biking", ExerciseCategory.IndoorCardio)
        {
            Id = templateId++,
            Description = "Convenient, low-impact, high resistance cardio."
        };
        bikingTemplate.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(bikingTemplate, indoorBiking) { Id = templateSetGroupId++ },
        };

        var templates = new List<Template>()
        {
            chestTemplate,
            backTemplate,
            legsCoreTemplate,
            armsShoulderTemplate,
            bikingTemplate
        };

        modelBuilder.Entity<Template>().HasData(templates);
    }
}
