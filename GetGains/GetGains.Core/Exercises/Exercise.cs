﻿using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Exercises;

public class Exercise
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    [Display(Name = "Body Part")]
    public string? BodyPart { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Media URL")]
    public string? MediaUrl { get; set; }

    public List<string>? Instructions { get; set; }

    public string? Author { get; set; }
}
