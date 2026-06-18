using Laboratorio12DAEA2.Models;
using Laboratorio12DAEA2.Request;
using Laboratorio12DAEA2.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio12DAEA2.Controllers
{
    [Route("api/cursos")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DemoContext _context;

        public CoursesController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<string> Create(CourseRequestV1 request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Description) ||
                request.Price <= 0 ||
                request.DurationHours <= 0 ||
                request.InstructorId <= 0)
            {
                return BadRequest("Los datos del curso son inválidos.");
            }

            var instructorExists = _context.Instructors
                .Any(instructor =>
                    instructor.InstructorId == request.InstructorId &&
                    !instructor.isDelete);

            if (!instructorExists)
            {
                return BadRequest("El instructor indicado no existe.");
            }

            try
            {
                var course = new Course
                {
                    Name = request.Name.Trim(),
                    Description = request.Description.Trim(),
                    Price = request.Price,
                    DurationHours = request.DurationHours,
                    InstructorId = request.InstructorId,
                    isDelete = false
                };

                _context.Courses.Add(course);
                _context.SaveChanges();

                return Ok("Curso registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar el curso: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<CourseResponseV1>> GetAll()
        {
            var courses = (
                from course in _context.Courses.AsNoTracking()
                join instructor in _context.Instructors.AsNoTracking()
                    on course.InstructorId equals instructor.InstructorId
                where !course.isDelete && !instructor.isDelete
                select new CourseResponseV1
                {
                    CourseId = course.CourseId,
                    Name = course.Name,
                    Description = course.Description,
                    Price = course.Price,
                    DurationHours = course.DurationHours,
                    InstructorId = instructor.InstructorId,
                    InstructorName = instructor.FirstName + " " + instructor.LastName
                })
                .ToList();

            return Ok(courses);
        }
    }
}
