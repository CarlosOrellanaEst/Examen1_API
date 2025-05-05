namespace examenAPI.Dtos.Course
{
    public class CourseUpdateDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? File { get; set; } // Added to handle image uploads
        public string? Schedule { get; set; }
        public string? Professor { get; set; }
    }
}
