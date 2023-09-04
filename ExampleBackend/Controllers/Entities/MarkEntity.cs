using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleBackend.Controllers.Entities
{
	public class MarkEntity
	{
        [Key]
        public int MarkId { get; set; }
		public int Mark { get; set; }
		public string? DisciplineName { get; set; }
    }
}

