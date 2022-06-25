using GetGains.Core.Enums;
using GetGains.Core.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace GetGains.Data.Services;

public class TemplateData : ITemplateData
{
    private readonly GainsDbContext context;

    public TemplateData(GainsDbContext context)
    {
        this.context = context
            ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddTemplate(Template template)
    {
        context.Templates.Add(template);
    }

    public void Delete(Template template)
    {
        throw new NotImplementedException();
    }

    public async Task<Template?> GetTemplateAsync(int id, bool populateSets)
    {
        var template = populateSets
            ? await context.Templates
                .Where(template => template.Id == id)
                .Include(template => template.GroupTemplates
                    .OrderBy(group => group.GroupNumber))
                    .ThenInclude(group => group.Exercise)
                .Include(template => template.GroupTemplates)
                    .ThenInclude(group => group.SetTemplates
                        .OrderBy(set => set.SetNumber))
                .FirstOrDefaultAsync()
            : await context.Templates
                .Where(template => template.Id == id)
                .FirstOrDefaultAsync();

        return template;
    }

    public async Task<List<Template>> GetTemplatesAsync(bool includeSets)
    {
        IQueryable<Template> query = includeSets
            ? context.Templates
                .Include(template => template.GroupTemplates
                    .OrderBy(group => group.GroupNumber))
                    .ThenInclude(group => group.Exercise)
                .Include(template => template.GroupTemplates)
                    .ThenInclude(group => group.SetTemplates)
                .OrderBy(template => template.Name)
            : context.Templates.OrderBy(template => template.Name);

        var templates = await query.ToListAsync();

        return templates;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }

    public void SeedData()
    {
        var benchPress = context.Exercises.First(exercise => exercise.Name == "Bench Press" && exercise.Category == ExerciseCategory.Barbell);
        var squat = context.Exercises.First(exercise => exercise.Name == "Squat");
        var pullUp = context.Exercises.First(exercise => exercise.Name == "Pull Up"); ;
        var overHeadPress = context.Exercises.First(exercise => exercise.Name == "Over Head Press");
        var dumbbellCurls = context.Exercises.First(exercise => exercise.Name == "Dumbbell Curls");
        var indoorBiking = context.Exercises.First(exercise => exercise.Name == "Indoor Biking");
        var weightedCrunch = context.Exercises.First(exercise => exercise.Name == "Weighted Crunch");
        var bentOverRows = context.Exercises.First(exercise => exercise.Name == "Bent Over Rows");
        var lateralRaises = context.Exercises.First(exercise => exercise.Name == "Lateral Raises");
        var calfRaises = context.Exercises.First(exercise => exercise.Name == "Calf Raises");
        var benchPressDB = context.Exercises.First(exercise => exercise.Name == "Bench Press" && exercise.Category == ExerciseCategory.Dumbbell);
        var arnoldPress = context.Exercises.First(exercise => exercise.Name == "Arnold Press");
        var dip = context.Exercises.First(exercise => exercise.Name == "Dip");

        var chestWorkout = new Template("Chest", ExerciseCategory.Barbell)
        {
            Description = "Focus on Chest and other pushing muscles."
        };

        chestWorkout.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(chestWorkout, benchPress),
            new TemplateSetGroup(chestWorkout, pullUp),
            new TemplateSetGroup(chestWorkout, overHeadPress)
        };

        var benchPressSet = chestWorkout.GroupTemplates[0];
        benchPressSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(benchPressSet)
            {
                SetNumber = 1,
                TargetReps = 10,
                TargetWeight = 45
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 2,
                TargetReps = 8,
                TargetWeight = 95
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 3,
                TargetReps = 6,
                TargetWeight = 115
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 4,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 5,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 6,
                TargetReps = 6,
                TargetWeight = 180
            },
            new TemplateSet(benchPressSet)
            {
                SetNumber = 7,
                TargetReps = 8,
                TargetWeight = 165
            },
        };

        var pullupSet = chestWorkout.GroupTemplates[1];
        pullupSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(pullupSet)
            {
                SetNumber = 1,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                SetNumber = 2,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                SetNumber = 3,
                TargetReps = 10
            },
            new TemplateSet(pullupSet)
            {
                SetNumber = 4,
                TargetReps = 10
            },
        };

        var overheadPressSet = chestWorkout.GroupTemplates[2];
        overheadPressSet.SetTemplates = new List<TemplateSet>()
        {
            new TemplateSet(overheadPressSet)
            {
                SetNumber = 1,
                TargetReps = 8,
                TargetWeight = 65
            },
            new TemplateSet(overheadPressSet)
            {
                SetNumber = 2,
                TargetReps = 8,
                TargetWeight = 95
            },
            new TemplateSet(overheadPressSet)
            {
                SetNumber = 3,
                TargetReps = 6,
                TargetWeight = 95
            },
            new TemplateSet(overheadPressSet)
            {
                SetNumber = 4,
                TargetReps = 6,
                TargetWeight = 95
            }
        };

        var backWorkout = new Template("Back", ExerciseCategory.Barbell)
        {
            Description = "Focus on Upper back and other pulling muscles."
        };
        backWorkout.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(backWorkout, bentOverRows),
            new TemplateSetGroup(backWorkout, overHeadPress),
            new TemplateSetGroup(backWorkout, dumbbellCurls),
        };

        var legsCoreWorkout = new Template("Legs and Core", ExerciseCategory.Barbell)
        {
            Description = "Focus on Leg foundations and Core strength."
        };
        legsCoreWorkout.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(legsCoreWorkout, squat),
            new TemplateSetGroup(legsCoreWorkout, calfRaises),
            new TemplateSetGroup(legsCoreWorkout, weightedCrunch),
        };

        var armsShoulderWorkout = new Template("Arms and Shoulders", ExerciseCategory.Dumbbell)
        {
            Description = "Focus on the \"accessory muscles\" that support most upper movement."
        };
        armsShoulderWorkout.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(armsShoulderWorkout, arnoldPress),
            new TemplateSetGroup(armsShoulderWorkout, dumbbellCurls),
            new TemplateSetGroup(armsShoulderWorkout, dip),
            new TemplateSetGroup(armsShoulderWorkout, pullUp),
        };

        var bikingWorkout = new Template("Biking", ExerciseCategory.IndoorCardio)
        {
            Description = "Convenient, low-impact, high resistance cardio."
        };
        bikingWorkout.GroupTemplates = new List<TemplateSetGroup>()
        {
            new TemplateSetGroup(bikingWorkout, indoorBiking),
        };

        var workouts = new List<Template>()
        {
            chestWorkout,
            backWorkout,
            legsCoreWorkout,
            armsShoulderWorkout,
            bikingWorkout
        };

        workouts.ForEach(workout =>
        {
            context.Add(workout);
        });
    }
}
