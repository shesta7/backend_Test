using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleBackend.Controllers.Entities
{
	public class UserEntity
	{
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}

