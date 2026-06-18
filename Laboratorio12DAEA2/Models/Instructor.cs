namespace Laboratorio12DAEA2.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool isDelete { get; set; } = false;
    }
}
