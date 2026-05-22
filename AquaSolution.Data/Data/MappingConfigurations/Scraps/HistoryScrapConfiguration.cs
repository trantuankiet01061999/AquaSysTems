
using AquaSolution.Data.Data.Entities.Admin;
using AquaSolution.Data.Data.Entities.Scraps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.Scraps
{
    public class HistoryScrapConfiguration : IEntityTypeConfiguration<HistoryScrap>
    {
        public void Configure(EntityTypeBuilder<HistoryScrap> builder)
        {
            builder.ToTable("tbl_HistoryScraps", schema: "ScrapManagement");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                 .HasConversion<string>();

            builder.HasOne<User>()
                 .WithMany()
                 .HasForeignKey(u => u.CreatedBy)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                 .WithMany()
                 .HasForeignKey(u => u.LastActionBy)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Factory>()
                 .WithMany()
                 .HasForeignKey(u => u.FactoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Department>()
              .WithMany()
              .HasForeignKey(u => u.DepartmentId)
              .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
