using Microsoft.EntityFrameworkCore;

namespace Laboratorio12DAEA2.Models
{

    //Es una clase que representa a la base de datos dentro del proyecto
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

    }
}
