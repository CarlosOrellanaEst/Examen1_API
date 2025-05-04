using System.ComponentModel.DataAnnotations;

namespace examenAPI.Dtos.Student
{
    public class StudentUpdateDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public int CourseId { get; set; }
    }
}
