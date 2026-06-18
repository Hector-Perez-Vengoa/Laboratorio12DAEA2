namespace Laboratorio12DAEA2.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DurationHours { get; set; }

        public int InstructorId { get; set; }

        public bool isDelete { get; set; } = false;
    }
}
