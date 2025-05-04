using System.ComponentModel.DataAnnotations;

namespace examenAPI.Dtos.Student
{
    public class StudentCreateDto
    {
        [Required]
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
