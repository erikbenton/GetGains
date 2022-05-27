using GetGains.Core.Enums;

namespace GetGains.Core.Extensions;

public static class ExerciseCategoryExtensions
{
    /// <summary>
    /// Gets the properly formatted label for the exercise category.
    /// </summary>
    /// <param name="exerciseType"></param>
    /// <returns>Exercise category string label.</returns>
    public static string GetLabel(this ExerciseCategory exerciseType)
    {
        return exerciseType switch
        {
            ExerciseCategory.Barbell => "Barbell",
            ExerciseCategory.Dumbbell => "Dumbbell",
            ExerciseCategory.LiftMachine => "Lift Machine",
            ExerciseCategory.Bodyweight => "Bodyweight",
            ExerciseCategory.OutdoorCardio => "Outdoor Cardio",
            ExerciseCategory.IndoorCardio => "Indoor Cardio",
            ExerciseCategory.MachineCardio => "Machine Cardio",
            ExerciseCategory.Other => "Other",
            _ => "N/A",
        };
    }
}
