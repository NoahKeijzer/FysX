using Microsoft.EntityFrameworkCore;
using Domain;

namespace EFInfrastructure
{
    public class FysioDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FysioTherapist> FysioTherapists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientFile> PatientFiles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<Treator> Treators { get; set; }
        public FysioDbContext(DbContextOptions<FysioDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasData(new Student("Bas Buijsen", "bbuijsen@gmail.com", 2170769));
        }

    }
}
