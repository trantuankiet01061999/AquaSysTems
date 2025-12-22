using AquaSolution.Data.Data.Entities;
using AquaSolution.Data.Data.Entities.ePAD;
using AquaSolution.Data.Data.Entities.KPI;
using AquaSolution.Data.Data.MappingConfigurations;
using AquaSolution.Data.Data.MappingConfigurations.KPI;
using AquaSolution.Data.KPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AquaSolution.Data.Connection
{
    public class ePADContext : DbContext
    {
        public ePADContext(DbContextOptions<ePADContext> options) : base(options)
        {

        }
        public DbSet<ePAD> ePAD { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ePAD>()
             .HasKey(x => x.EmployeeATID);

        }
    }
}
