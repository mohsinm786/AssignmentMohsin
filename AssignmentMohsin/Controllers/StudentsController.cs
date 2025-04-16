using AssignmentMohsin.Data;
using AssignmentMohsin.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMohsin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPost("{id}/assign-courses")]
        public async Task<IActionResult> AssignCourses(int id, [FromBody] List<int> courseIds)
        {
            var student = await _context.Students.Include(s => s.StudentCourses).FirstOrDefaultAsync(x => x.Id == id);

            if (student == null) return NotFound("Student not found");

            foreach (var course in courseIds)
            {
                if (!_context.Courses.Any(c => c.Id == course))
                {
                    return BadRequest("Course ID not found");
                }

                if (!student.StudentCourses.Any(x => x.CourseId == course))
                {
                    student.StudentCourses.Add(new StudentCourse
                    {
                        StudentId = student.Id,
                        CourseId = course
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Courses assigned!");
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.Include(s => s.StudentCourses).ThenInclude(sc => sc.Course)
                           .Select(s => new
                           {
                               s.Name,
                               s.Email,
                               s.Phone,
                               Courses = string.Join(", ", s.StudentCourses.Select(sc => sc.Course.CourseName))
                           }).ToListAsync();

            return Ok(students);
        }
    }
}
