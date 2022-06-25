using GetGains.Core.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Templates;

public class TemplateSetDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int SetNumber { get; set; }

    public int? TargetReps { get; set; }

    public double? TargetWeight { get; set; }

    public double? TargetTime { get; set; }

    public int? WarmUpTime { get; set; }

    public int? CoolDownTime { get; set; }

    [MaxLength(255)]
    public string? Note { get; set; }

    public TemplateSetDto(TemplateSet templateSet)
    {
        Id = templateSet.Id;
        SetNumber = templateSet.SetNumber;
        TargetReps = templateSet.TargetReps;
        TargetWeight = templateSet.TargetWeight;
        TargetTime = templateSet.TargetTime;
        WarmUpTime = templateSet.WarmUpTime;
        CoolDownTime = templateSet.CoolDownTime;
        Note = templateSet.Note;
    }
}

