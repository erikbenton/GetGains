using GetGains.Core.Models.Instructions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GetGains.API.Dtos.Instructions;

public class InstructionDto
{
    public int? Id { get; set; }

    [Required]
    public int StepNumber { get; set; }

    [Required]
    public string Text { get; set; }

    [JsonConstructor]
    public InstructionDto(int stepNumber, string text)
    {
        StepNumber = stepNumber;
        Text = text;
    }

    public InstructionDto(Instruction instruction)
    {
        Id = instruction.Id;
        StepNumber = instruction.StepNumber;
        Text = instruction.Text;
    }
}
