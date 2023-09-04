using System;
namespace ExampleBackend.Models;

public class AddMarksRequestValidationResult
{
	public required bool IsSuccess { get; init; }
	public List<string> Errors { get; set; } = new List<string>();
}

