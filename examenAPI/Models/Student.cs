using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examenAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required string Phone { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public required Course Course { get; set; }
    }
}
