using Microsoft.AspNetCore.Mvc;
using examenAPI.Data;
using examenAPI.Models;
using Microsoft.EntityFrameworkCore;
using examenAPI.Dtos.Student;

namespace examenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<StudentReadDto>> GetStudents()
        {
            var students = _context.Students
                                   .Include(s => s.Course)
                                   .Select(s => new StudentReadDto
                                   {
                                       Id = s.Id,
                                       Name = s.Name,
                                       Email = s.Email,
                                       Phone = s.Phone,
                                       CourseName = s.Course.Name
                                   })
                                   .ToList();

            return Ok(students);
        }

        // POST: api/Student
        [HttpPost]
        public ActionResult<StudentReadDto> CreateStudent([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                CourseId = dto.CourseId
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            var studentRead = new StudentReadDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                CourseName = _context.Courses.Find(dto.CourseId)?.Name
            };

            return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, studentRead);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentUpdateDto dto)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            student.Name = dto.Name ?? student.Name;
            student.Email = dto.Email ?? student.Email;
            student.Phone = dto.Phone ?? student.Phone;
            student.CourseId = dto.CourseId != 0 ? dto.CourseId : student.CourseId;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
