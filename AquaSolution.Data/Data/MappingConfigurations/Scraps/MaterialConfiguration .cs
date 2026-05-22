
using AquaSolution.Data.Entities.Scraps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.Scraps
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("tbl_Materials", schema: "ScrapManagement");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Plant)
                .HasConversion<string>();
        }
    }
}
