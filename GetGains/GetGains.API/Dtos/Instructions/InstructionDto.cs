using GetGains.Core.Models.Instructions;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Instructions;

public class InstructionDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int StepNumber { get; set; }

    [Required]
    public string Text { get; set; } = "";

    public InstructionDto(Instruction instruction)
    {
        Id = instruction.Id;
        StepNumber = instruction.StepNumber;
        Text = instruction.Text;
    }
}
