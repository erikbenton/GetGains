using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GetGains.Data.Services;

public class InMemExerciseData : IExerciseData
{
    private readonly GainsDbContext context;

    public InMemExerciseData(GainsDbContext context)
    {
        this.context = context;
    }

    public Exercise Add(Exercise exercise)
    {
        int stepNumber = 1;
        exercise.Instructions?.ForEach(instruction =>
        {
            instruction.StepNumber = stepNumber++;
        });

        context.Exercises.Add(exercise);
        context.SaveChanges();

        return exercise;
    }

    public bool Delete(Exercise exercise)
    {
        context.Exercises.Remove(exercise);

        context.SaveChanges();

        return true;
    }

    public List<Exercise> GetAll()
    {
        return GetAll(false);
    }

    public List<Exercise> GetAll(bool populateInstructions = false)
    {
        var exercises = populateInstructions
            ? context.Exercises
                .OrderBy(exer => exer.Name)
                .Include(exer => exer.Instructions)
                .ToList()
            : context.Exercises
                .OrderBy(exer => exer.Name)
                .ToList();

        exercises.ForEach(exer =>
        {
            exer.Instructions = exer.Instructions?
                .OrderBy(instr => instr.StepNumber).ToList();
        });

        return exercises;
    }

    public Exercise? GetExercise(int id)
    {
        return GetExercise(id, true);
    }

    public Exercise? GetExercise(int id, bool populateExercise = true)
    {
        var exercise = context.Exercises
            .Include(e => e.Instructions)
            .FirstOrDefault(e => e.Id == id);

        if (exercise is null) return null;

        if (populateExercise == false)
        {
            exercise.Instructions = new List<Instruction>();
        }

        exercise.Instructions = exercise.Instructions?
            .OrderBy(instr => instr.StepNumber).ToList();

        return exercise;
    }

    public bool Update(Exercise exercise)
    {
        MarkEntityModified(exercise);

        UpdateInstructions(exercise);

        context.SaveChanges();

        return true;
    }

    private void UpdateInstructions(Exercise exercise)
    {
        if (exercise.Instructions is null) return;

        RemoveDeletedInstructions(exercise);
        IndexInstructions(exercise.Instructions);
        SaveInstructions(exercise.Instructions);
    }

    private static void IndexInstructions(List<Instruction> instructions)
    {
        int stepNumber = 1;
        instructions.ForEach(instr =>
        {
            instr.StepNumber = stepNumber++;
        });
    }

    private void SaveInstructions(List<Instruction> instructions)
    {
        instructions.ForEach(instr =>
        {
            SaveInstruction(instr);
        });
    }

    private void SaveInstruction(Instruction instruction)
    {
        if (instruction.IsNewEntry)
        {
            context.Instructions.Add(instruction);
        }
        else
        {
            MarkEntityModified(instruction);
        }
    }

    private void RemoveDeletedInstructions(Exercise exercise)
    {
        if (exercise.Instructions is null) return;

        // Get the instruction Ids saved in database
        var instructionsInDb = context.Instructions
            .Where(instr => instr.Exercise.Id == exercise.Id)
            .AsNoTracking()
            .ToList();

        // Delete instructions with Ids only in database
        instructionsInDb?
            .Except(exercise.Instructions, new InstructionIDComparer())
            .ToList()
            .ForEach(instr =>
            {
                MarkEntityDeleted(instr);
            });
    }

    private void MarkEntityModified<T>(T entity)
    {
        if (entity is null) return;

        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
    }

    private void MarkEntityDeleted<T>(T entity)
    {
        if (entity is null) return;

        var entry = context.Entry(entity);
        entry.State = EntityState.Deleted;
    }

    public static void SeedData(IExerciseData context)
    {
        var benchPress = new Exercise()
        {
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up from your chest",
            Instructions = null,
            MediaUrl = "https://images.unsplash.com/photo-1534368959876-26bf04f2c947",
            Author = "Erik Benton"
        };
        var squat = new Exercise()
        {
            Name = "Squat",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.Barbell,
            Description = "Stand up with the barbell over your shoulders",
            Instructions = null,
            MediaUrl = "https://images.unsplash.com/photo-1596357395217-80de13130e92",
            Author = "Kyle McNurmann"
        };
        var pullUp = new Exercise()
        {
            Name = "Pull Up",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Bodyweight,
            Description = "Pull yourself up over the bar",
            Instructions = null,
            MediaUrl = "https://images.unsplash.com/photo-1597347316205-36f6c451902a",
            Author = "Alexa Stils"
        };
        var overHeadPress = new Exercise()
        {
            Name = "Over Head Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Barbell,
            Description = "Press the bar up over your head",
            Instructions = null,
            MediaUrl = "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3",
            Author = "Jeff Lovely"
        };
        var dumbbellCurls = new Exercise()
        {
            Name = "Dumbbell Curls",
            BodyPart = BodyPart.Biceps,
            Category = ExerciseCategory.Dumbbell,
            Description = "Curl the dumbbell up from your waist to your shoulder",
            Instructions = null,
            Author = "Mike Vincent"
        };
        var indoorBiking = new Exercise()
        {
            Name = "Indoor Biking",
            BodyPart = BodyPart.Legs,
            Category = ExerciseCategory.IndoorCardio,
            Description = "Bike on a bike trainer",
            Instructions = null,
            Author = "Sarah Bennet"
        };
        var weightedCrunch = new Exercise()
        {
            Name = "Weighted Crunch",
            BodyPart = BodyPart.Core,
            Category = ExerciseCategory.Dumbbell,
            Description = "Crunches with heavy dumbbell or plate",
            Instructions = null,
            Author = "Josh Sylar"
        };
        var bentOverRows = new Exercise()
        {
            Name = "Bent Over Rows",
            BodyPart = BodyPart.UpperBack,
            Category = ExerciseCategory.Barbell,
            Description = "Bend over and row the barbell",
            Instructions = null,
            Author = "Nathan Trant"
        };
        var lateralRaises = new Exercise()
        {
            Name = "Lateral Raises",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Raise fully extended arms with dumbbells in them",
            Instructions = null,
            Author = "Tasha Heffle"
        };
        var calfRaises = new Exercise()
        {
            Name = "Calf Raises",
            BodyPart = BodyPart.Calves,
            Category = ExerciseCategory.Barbell,
            Description = "Hold barbell in hands while standing on tippy-toes",
            Instructions = null,
            Author = "Becky Yeung"
        };
        var benchPressDB = new Exercise()
        {
            Name = "Bench Press",
            BodyPart = BodyPart.Chest,
            Category = ExerciseCategory.Dumbbell,
            Description = "Bench press with dumbbells",
            Instructions = null,
            Author = "Charles Orbeiter"
        };
        var arnoldPress = new Exercise()
        {
            Name = "Arnold Press",
            BodyPart = BodyPart.Shoulders,
            Category = ExerciseCategory.Dumbbell,
            Description = "Dumbbell shoulder press with full shoulder rotation",
            Instructions = null,
        };
        var dip = new Exercise()
        {
            Name = "Dip",
            BodyPart = BodyPart.Triceps,
            Category = ExerciseCategory.Bodyweight,
            Description = "Hang so that you can slowly lower/dip your body using your arms/triceps",
            Instructions = null,
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
        exercises.ForEach(exercise => context.Add(exercise));
    }

    // TODO Find better location for comparer when/after implementing sql
    private class InstructionIDComparer : EqualityComparer<Instruction>
    {
        public override bool Equals(Instruction? x, Instruction? y)
        {
            if (x == null || y == null) return false;

            return x.Id == y.Id;
        }

        public override int GetHashCode([DisallowNull] Instruction obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

