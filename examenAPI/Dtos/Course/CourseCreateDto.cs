using System.ComponentModel.DataAnnotations;

namespace examenAPI.Dtos.Course
{
    public class CourseCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Schedule { get; set; }
        public string Professor { get; set; }
    }
}
