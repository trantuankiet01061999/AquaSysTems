using AquaSolution.Data.Data.Entities.Admin;
using AquaSolution.Data.Data.Entities.Scraps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.Scraps
{
    public class RequestApprovalConfiguration : IEntityTypeConfiguration<RequestApproval>
    {
        public void Configure(EntityTypeBuilder<RequestApproval> builder)
        {
            builder.ToTable("tbl_RequestApprovals", schema: "ScrapManagement");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                 .HasConversion<string>();

            builder.HasOne<HistoryScrap>()
                 .WithMany()
                 .HasForeignKey(u => u.HistoryScrapId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
              .WithMany()
              .HasForeignKey(u => u.ActionBy)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
              .WithMany()
              .HasForeignKey(u => u.DecisionMaker)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
