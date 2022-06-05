﻿using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using Microsoft.EntityFrameworkCore;

namespace GetGains.Data.Services;

public static class InMemExerciseState
{
    public static bool HasNoData = true;
}

public class InMemExerciseData : IExerciseData
{
    private readonly GainsDbContext context;

    public InMemExerciseData(GainsDbContext context)
    {
        this.context = context;

        if (InMemExerciseState.HasNoData)
        {
            SeedData(context);
            InMemExerciseState.HasNoData = false;
        }
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
        var exercises = context.Exercises.OrderBy(e => e.Name).ToList();

        if (populateInstructions)
        {
            exercises.ForEach(exer =>
            {
                exer.Instructions = context.Instructions
                    .Where(instr => instr.Exercise.Id == exer.Id)
                    .ToList();
            });
        }

        return exercises;
    }

    public Exercise? GetById(int id)
    {
        return GetById(id, true);
    }

    public Exercise? GetById(int id, bool populateExercise = true)
    {
        var exercise = context.Exercises.FirstOrDefault(e => e.Id == id);

        if (exercise is null) return null;

        if (populateExercise)
        {
            exercise.Instructions = context.Instructions
                .Where(instr => instr.Exercise.Id == exercise.Id)
                .ToList();
        }

        return exercise;
    }

    public bool Update(Exercise exercise)
    {
        var entry = context.Entry(exercise);
        entry.State = EntityState.Modified;

        var savedInstructions = context.Instructions
            .Where(instr => instr.Exercise.Id == exercise.Id)
            .AsNoTracking()
            .ToList();

        if (exercise.Instructions is not null)
        {
            var instructionIdsToRemove = savedInstructions?
                .Select(instr => instr.Id)
                .Except(exercise.Instructions
                    .Select(instr => instr.Id))
                .ToList();

            savedInstructions?.ForEach(instr =>
            {
                if (instructionIdsToRemove != null && instructionIdsToRemove.Contains(instr.Id))
                {
                    var entry = context.Entry(instr);
                    entry.State = EntityState.Deleted;
                }
            });

            int stepNumber = 1;
            exercise.Instructions.ForEach(instr =>
            {
                instr.StepNumber = stepNumber++;
                if (instr.Id == 0)
                {
                    context.Instructions.Add(instr);
                }
                else
                {
                    var entry = context.Entry(instr);
                    entry.State = EntityState.Modified;
                }
            });
        }

        context.SaveChanges();

        return true;
    }

    public static void SeedData(GainsDbContext context)
    {
        var _exercises = new List<Exercise>()
        {
            new Exercise()
            {
                Name = "Bench Press",
                BodyPart = BodyPart.Chest,
                Category = ExerciseCategory.Barbell,
                Description = "Press the bar up from your chest",
                Instructions = null,
                MediaUrl = "https://images.unsplash.com/photo-1534368959876-26bf04f2c947",
                Author = "Erik Benton"
            },
            new Exercise()
            {
                Name = "Squat",
                BodyPart = BodyPart.Legs,
                Category = ExerciseCategory.Barbell,
                Description = "Stand up with the barbell over your shoulders",
                Instructions = null,
                MediaUrl = "https://images.unsplash.com/photo-1596357395217-80de13130e92",
                Author = "Kyle McNurmann"
            },
            new Exercise()
            {
                Name = "Pull Up",
                BodyPart = BodyPart.UpperBack,
                Category = ExerciseCategory.Bodyweight,
                Description = "Pull yourself up over the bar",
                Instructions = null,
                MediaUrl = "https://images.unsplash.com/photo-1597347316205-36f6c451902a",
                Author = "Alexa Stils"
            },
            new Exercise()
            {
                Name = "Over Head Press",
                BodyPart = BodyPart.Shoulders,
                Category = ExerciseCategory.Barbell,
                Description = "Press the bar up over your head",
                Instructions = null,
                MediaUrl = "https://images.unsplash.com/photo-1532029837206-abbe2b7620e3",
                Author = "Jeff Lovely"
            },
            new Exercise()
            {
                Name = "Dumbbell Curls",
                BodyPart = BodyPart.Biceps,
                Category = ExerciseCategory.Dumbbell,
                Description = "Curl the dumbbell up from your waist to your shoulder",
                Instructions = null,
                Author = "Mike Vincent"
            },
            new Exercise()
            {
                Name = "Indoor Biking",
                BodyPart = BodyPart.Legs,
                Category = ExerciseCategory.IndoorCardio,
                Description = "Bike on a bike trainer",
                Instructions = null,
                Author = "Sarah Bennet"
            },
        };

        _exercises.ForEach(exercise =>
        {
            exercise.Instructions = new List<Instruction>()
            {
                new Instruction(exercise)
                {
                    StepNumber = 1,
                    Text = "Warm up"
                },
                new Instruction(exercise)
                {
                    StepNumber = 2,
                    Text = "Perform the exercise",
                },
                new Instruction(exercise)
                {
                    StepNumber = 3,
                    Text = "Clean up the workout area",
                }
            };
        });

        // Add them all to the context for now
        _exercises.ForEach(exercise =>
        {
            context.Exercises.Add(exercise);
            exercise.Instructions?.ForEach(instruction => context
                .Instructions
                .Add(instruction));
        });

        context.SaveChanges();
    }
}
