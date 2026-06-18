namespace Laboratorio12DAEA2.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public bool isDelete { get; set; } = false;
    }
}
