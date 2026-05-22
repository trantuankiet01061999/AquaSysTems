using AquaSolution.Data.Data.Entities.Admin;
using AquaSolution.Data.Data.Entities.Scraps;
using AquaSolution.Data.Entities.Scraps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.Scraps
{
    public class WeightConfiguration : IEntityTypeConfiguration<Weight>
    {
        public void Configure(EntityTypeBuilder<Weight> builder)
        {
            builder.ToTable("tbl_Weights", schema: "ScrapManagement");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.WeightValue)
                 .HasColumnType("decimal(18,4)")
                 .HasDefaultValue(0m);

            builder.HasOne<Material>()
                 .WithMany()
                 .HasForeignKey(u => u.MaterialId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>()
              .WithMany()
              .HasForeignKey(u => u.CreatedBy)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
