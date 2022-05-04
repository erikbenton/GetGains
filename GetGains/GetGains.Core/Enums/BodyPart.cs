using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Enums;

public enum BodyPart
{
    [Display(Name = "Chest")]
    Chest,
    [Display(Name = "Legs")]
    Legs,
    [Display(Name = "Calves")]
    Calves,
    [Display(Name = "Upper Back")]
    UpperBack,
    [Display(Name = "Lower Back")]
    LowerBack,
    [Display(Name = "Shoulders")]
    Shoulders,
    [Display(Name = "Biceps")]
    Biceps,
    [Display(Name = "Triceps")]
    Triceps,
    [Display(Name = "Forearms")]
    Forearms,
    [Display(Name = "Core")]
    Core,
    [Display(Name = "Other")]
    Other,
}
