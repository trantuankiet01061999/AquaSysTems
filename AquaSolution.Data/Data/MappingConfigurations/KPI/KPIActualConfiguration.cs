using AquaSolution.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.KPI
{
    public class KPIActualConfiguration : IEntityTypeConfiguration<KPIActual>
    {
        public void Configure(EntityTypeBuilder<KPIActual> builder)
        {
            builder.ToTable("tbl_KPIActuals");
            builder.HasKey(e => e.Id);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<KPITarget>()
                  .WithMany()
                  .HasForeignKey(e => e.KPITargetId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<KPITotalScore>()
                 .WithMany()
                 .HasForeignKey(e => e.KPITotalScoreId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.Property(d => d.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");

        }
    }
}
