using Microsoft.EntityFrameworkCore;
using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.DAL.Persistence
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options)
        : base(options)
        {
        }

        public DbSet<Teacher>? Teacher { get; set; }
        public DbSet<Student>? Student { get; set; }
        public DbSet<Course>? Course { get; set; }
    }
}
