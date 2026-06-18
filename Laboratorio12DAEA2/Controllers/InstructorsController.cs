using Laboratorio12DAEA2.Models;
using Laboratorio12DAEA2.Request;
using Laboratorio12DAEA2.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Laboratorio12DAEA2.Controllers
{
    [Route("api/instructores")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly DemoContext _context;

        public InstructorsController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<string> Create(InstructorRequestV1 request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Specialty) ||
                !MailAddress.TryCreate(request.Email, out _))
            {
                return BadRequest("Los datos del instructor son inválidos.");
            }

            try
            {
                var instructor = new Instructor
                {
                    FirstName = request.FirstName.Trim(),
                    LastName = request.LastName.Trim(),
                    Specialty = request.Specialty.Trim(),
                    Email = request.Email.Trim(),
                    isDelete = false
                };

                _context.Instructors.Add(instructor);
                _context.SaveChanges();

                return Ok("Instructor registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar el instructor: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<InstructorResponseV1>> GetAll()
        {
            var instructors = _context.Instructors
                .AsNoTracking()
                .Where(instructor => !instructor.isDelete)
                .Select(instructor => new InstructorResponseV1
                {
                    InstructorId = instructor.InstructorId,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Specialty = instructor.Specialty,
                    Email = instructor.Email
                })
                .ToList();

            return Ok(instructors);
        }

        [HttpGet("{id:int}")]
        public ActionResult<InstructorResponseV1> GetById(int id)
        {
            var instructor = _context.Instructors
                .AsNoTracking()
                .Where(instructor => instructor.InstructorId == id && !instructor.isDelete)
                .Select(instructor => new InstructorResponseV1
                {
                    InstructorId = instructor.InstructorId,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Specialty = instructor.Specialty,
                    Email = instructor.Email
                })
                .FirstOrDefault();

            if (instructor == null)
            {
                return Ok("Instructor no encontrado.");
            }

            return Ok(instructor);
        }
    }
}
