using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace examenAPI.Models
{
    public class Course
    {
        public int Id {get; set;}

        [Required]
        public required string Name { get; set; }

        public required string Description { get; set; }
        public required string ImageUrl { get; set; } = string.Empty;
        public required string Schedule { get; set; }
        public required string Professor { get; set; }
        public required ICollection<Student> Students { get; set; }
     //   public string? Image { get; set; }
    }
}
