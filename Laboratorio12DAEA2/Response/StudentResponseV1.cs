namespace Laboratorio12DAEA2.Response
{
    public class StudentResponseV1
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
