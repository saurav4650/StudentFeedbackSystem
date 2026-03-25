using Microsoft.EntityFrameworkCore;

namespace StudentFeedbackSystem.EntityModels
{
    public class StudentFeedbackDbContext : DbContext
    {
        public StudentFeedbackDbContext(DbContextOptions<StudentFeedbackDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSchedule> CourseSchedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<FeedbackForm> FeedbackForms { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<FeedbackResponse> FeedbackResponses { get; set; }
        public DbSet<TblLog> TblLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseSchedule>()
                .HasOne(cs => cs.Instructor)
                .WithMany(u => u.InstructorSchedules)
                .HasForeignKey(cs => cs.InstructorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Schedule)
                .WithMany(cs => cs.Enrollments)
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackForm>()
                .HasOne(f => f.Student)
                .WithMany(u => u.FeedbackForms)
                .HasForeignKey(f => f.StudentUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackForm>()
                .HasOne(f => f.Schedule)
                .WithMany(cs => cs.FeedbackForms)
                .HasForeignKey(f => f.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackResponse>()
                .HasOne(fr => fr.FeedbackForm)
                .WithMany(f => f.FeedbackResponses)
                .HasForeignKey(fr => fr.FeedbackFormId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackResponse>()
                .HasOne(fr => fr.Question)
                .WithMany(q => q.FeedbackResponses)
                .HasForeignKey(fr => fr.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
