using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TSP_PROJECT.Models;

namespace TSP_PROJECT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TSP_PROJECT.Models.Attendance> Attendance { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Batch> Batch { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Course> Course { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.CourseContent> CourseContent { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.CourseCoordinator> CourseCoordinator { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Exam> Exam { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Lab> Lab { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Payment> Payment { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Result> Result { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Schedule> Schedule { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Student> Student { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.StudentBatch> StudentBatch { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.Trainer> Trainer { get; set; } = default!;
        public DbSet<TSP_PROJECT.Models.TrainerCourse> TrainerCourse { get; set; } = default!;
    }
}
