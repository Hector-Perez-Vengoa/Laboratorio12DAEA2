namespace Laboratorio12DAEA2.Request
{
    public class CourseRequestV1
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DurationHours { get; set; }

        public int InstructorId { get; set; }
    }
}
