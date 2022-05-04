using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Enums;

public enum ExerciseCategory
{
    [Display(Name = "Barbell")]
    Barbell,
    [Display(Name = "Dumbbell")]
    Dumbbell,
    [Display(Name = "Lift Machine")]
    LiftMachine,
    [Display(Name = "Bodyweight")]
    Bodyweight,
    [Display(Name = "Outdoor Cardio")]
    OutdoorCardio,
    [Display(Name = "Indoor Cardio")]
    IndoorCardio,
    [Display(Name = "Machine Cardio")]
    MachineCardio,
    [Display(Name = "Other")]
    Other,
}