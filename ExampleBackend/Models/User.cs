using System;
namespace ExampleBackend.Models;

public class User
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string MiddleName { get; init; }

    public List<Mark> Marks = new List<Mark>();
}

