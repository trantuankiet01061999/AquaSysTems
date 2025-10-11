using AquaSolution.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.KPI
{
    public class KPITotalScoreConfiguration : IEntityTypeConfiguration<KPITotalScore>
    {
        public void Configure(EntityTypeBuilder<KPITotalScore> builder)
        {
            builder.ToTable("tbl_KPITotalScores");
            builder.HasKey(e => e.Id);
  
            builder.HasOne<KPIRequest>()
                     .WithMany()
                     .HasForeignKey(u => u.KPIRequestId)
                     .OnDelete(DeleteBehavior.Restrict);
            builder.Property(d => d.CreatedDate)
                       .IsRequired()
                       .HasDefaultValueSql("GETDATE()");
        }
    }
}
