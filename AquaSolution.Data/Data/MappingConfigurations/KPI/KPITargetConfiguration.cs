using AquaSolution.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations.KPI
{
    public class KPITargetConfiguration : IEntityTypeConfiguration<KPITarget>
    {
        public void Configure(EntityTypeBuilder<KPITarget> builder)
        {
            builder.ToTable("tbl_KPITargets");
            builder.HasKey(e => e.Id);
            builder.HasOne<UserTask>()
                .WithMany()
                .HasForeignKey(e => e.UserTaskId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(d => d.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()");

        }
    }
}
