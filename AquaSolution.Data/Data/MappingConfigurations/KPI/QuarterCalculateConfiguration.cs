using AquaSolution.Data.Data.Entities.KPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.KPI
{
    public class QuarterCalculateConfiguration : IEntityTypeConfiguration<QuarterCalculate>
    {
        public void Configure(EntityTypeBuilder<QuarterCalculate> builder)
        {
            builder.ToTable("tbl_QuarterCalculates", schema: "KPI");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.QuarterCalculated).IsRequired().HasMaxLength(100);
            builder.Property(e => e.QuarterCalculateType)
                    .HasConversion<string>()
                    .IsRequired()
                    .HasMaxLength(100);
        }
    }
}
