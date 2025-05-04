namespace examenAPI.Dtos.Course
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required string Schedule { get; set; }
        public required string Professor { get; set; }
    }
}
