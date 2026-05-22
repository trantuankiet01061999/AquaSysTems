using AquaSolution.Data.Data.Entities.Admin;
using AquaSolution.Data.Data.Entities.Scraps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.Scraps
{
    public class FlowApprovalScrapConfiguration : IEntityTypeConfiguration<FlowApprovalScrap>
    {
        public void Configure(EntityTypeBuilder<FlowApprovalScrap> builder)
        {
            builder.ToTable("tbl_FlowApprovalScraps", schema: "ScrapManagement");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                            .IsRequired()
                            .HasMaxLength(500);

            builder.Property(x => x.Description)
                .HasMaxLength(2500);

            builder.Property(x => x.Step)
                .IsRequired();

        }
    }
}
