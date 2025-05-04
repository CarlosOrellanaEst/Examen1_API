using System.ComponentModel.DataAnnotations;

namespace CursosApi.Dtos.Student
{
    public class StudentCreateDto
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
