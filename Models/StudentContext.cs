using Microsoft.EntityFrameworkCore;

namespace StudentApi.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options){}

        public DbSet<Student> StudentTable { get; set; }
        public DbSet<Subject> SubjectTable { get; set; }
        public DbSet<Sports> SportsTable { get; set; }


    }
}