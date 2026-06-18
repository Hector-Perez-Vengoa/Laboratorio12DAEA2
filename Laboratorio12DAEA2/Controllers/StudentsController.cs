using Laboratorio12DAEA2.Models;
using Laboratorio12DAEA2.Request;
using Laboratorio12DAEA2.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Laboratorio12DAEA2.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DemoContext _context;

        public StudentsController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<string> Create(StudentRequestV1 request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Phone) ||
                !MailAddress.TryCreate(request.Email, out _))
            {
                return BadRequest("Los datos del estudiante son inválidos.");
            }

            try
            {
                var student = new Student
                {
                    FirstName = request.FirstName.Trim(),
                    LastName = request.LastName.Trim(),
                    Email = request.Email.Trim(),
                    Phone = request.Phone.Trim(),
                    RegistrationDate = DateTime.Now,
                    isDelete = false
                };

                _context.Students.Add(student);
                _context.SaveChanges();

                return Ok("Estudiante registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar el estudiante: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<StudentResponseV1>> GetAll()
        {
            var students = _context.Students
                .AsNoTracking()
                .Where(student => !student.isDelete)
                .Select(student => new StudentResponseV1
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    RegistrationDate = student.RegistrationDate
                })
                .ToList();

            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public ActionResult<StudentResponseV1> GetById(int id)
        {
            var student = _context.Students
                .AsNoTracking()
                .Where(student => student.StudentId == id && !student.isDelete)
                .Select(student => new StudentResponseV1
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    RegistrationDate = student.RegistrationDate
                })
                .FirstOrDefault();

            if (student == null)
            {
                return Ok("Estudiante no encontrado.");
            }

            return Ok(student);
        }
    }
}
