using Microsoft.EntityFrameworkCore;
using Domain;

namespace ApiInfrastructure
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
