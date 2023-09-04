using System;
namespace ExampleBackend.Models;

public class AddMarkRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? MiddleName { get; init; }

    public List<Mark> Marks { get; set; } = new List<Mark>();
}

