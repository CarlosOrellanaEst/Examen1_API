using System.ComponentModel.DataAnnotations;

namespace examenAPI.Dtos.Course
{
    public class CourseCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public required string Description { get; set; }
        public required string Schedule { get; set; }
        public required string Professor { get; set; }
        [Required]
        public required IFormFile File { get; set; }
    }
}
