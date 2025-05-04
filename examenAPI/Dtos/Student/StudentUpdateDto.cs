using System.ComponentModel.DataAnnotations;

namespace examenAPI.Dtos.Student
{
    public class StudentUpdateDto
    {
        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Phone { get; set; }

        public int CourseId { get; set; }
    }
}
