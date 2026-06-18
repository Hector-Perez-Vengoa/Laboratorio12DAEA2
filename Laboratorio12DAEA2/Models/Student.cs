namespace Laboratorio12DAEA2.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public bool isDelete { get; set; } = false;
    }
}
