namespace examenAPI.Dtos.Student
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string CourseName { get; set; }
    }
}
