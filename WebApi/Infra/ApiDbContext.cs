using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Infra
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> contextOptions) : base(contextOptions)
        {

        }

    }
}
