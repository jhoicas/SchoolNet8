using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }


        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    }
}
