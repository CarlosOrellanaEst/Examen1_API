using Microsoft.AspNetCore.Mvc;
using examenAPI.Data;
using examenAPI.Models;
using Microsoft.EntityFrameworkCore;
using examenAPI.Dtos.Course;
using examenAPI.Validators;
using examenAPI.Dtos.Student; // Added missing using directive for StudentBasicReadDto

namespace examenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "CoursesImages");

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public ActionResult<IEnumerable<CourseReadDto>> GetCourses()
        {
            var courses = _context.Courses
                                  .Select(c => new CourseReadDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Description = c.Description,
                                      ImageUrl = c.ImageUrl,
                                      Schedule = c.Schedule,
                                      Professor = c.Professor
                                  })
                                  .ToList();

            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public ActionResult<CourseReadDto> GetCourse(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            var dto = new CourseReadDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                Schedule = course.Schedule,
                Professor = course.Professor
            };

            return Ok(dto);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No se ha proporcionado una imagen válida.");

            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                Schedule = dto.Schedule,
                Professor = dto.Professor,
                Students = new List<Student>(),
                ImageUrl = string.Empty // Initialize ImageUrl with a default value
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var fileName = course.Id.ToString() + Path.GetExtension(dto.File.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            course.ImageUrl = fileName;
            await _context.SaveChangesAsync();

            var courseRead = new CourseReadDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                Schedule = course.Schedule,
                Professor = course.Professor
            };

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, courseRead);
        }

        // PUT: api/Course/5
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CourseUpdateDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = dto.Name ?? course.Name;
            course.Description = dto.Description ?? course.Description;
            course.Schedule = dto.Schedule ?? course.Schedule;
            course.Professor = dto.Professor ?? course.Professor;

            if (dto.File != null && dto.File.Length > 0)
            {
                var fileName = course.Id.ToString() + Path.GetExtension(dto.File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                course.ImageUrl = fileName;
            }

            await _context.SaveChangesAsync();

            return Ok(new CourseReadDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                Schedule = course.Schedule,
                Professor = course.Professor
            });
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
                return NotFound();

            // Delete the image file if it exists
            if (!string.IsNullOrEmpty(course.ImageUrl))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages", course.ImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }

        // GET: api/Course/with-students
        [HttpGet("with-students")]
        public async Task<IActionResult> GetCoursesWithStudents()
        {
            var courses = await _context.Courses
                .Include(c => c.Students)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    c.ImageUrl,
                    c.Schedule,
                    c.Professor,
                    Students = c.Students.Select(s => new StudentBasicReadDto
                    {
                        Name = s.Name,
                        Email = s.Email
                    }).ToList()
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/Course/{id}/with-students
        [HttpGet("{id}/with-students")]
        public async Task<IActionResult> GetCourseWithStudents([FromRoute] int id)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var courseWithStudents = new
            {
                course.Id,
                course.Name,
                course.Description,
                course.ImageUrl,
                course.Schedule,
                course.Professor,
                Students = course.Students.Select(s => new StudentBasicReadDto
                {
                    Name = s.Name,
                    Email = s.Email
                }).ToList()
            };

            return Ok(courseWithStudents);
        }
    }
}
