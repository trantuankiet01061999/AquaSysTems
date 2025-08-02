using AquaSolution.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaSolution.Data.Data.MappingConfigurations
{
    public class SysTemHistoryConfiguration : IEntityTypeConfiguration<SysTemHistory>
    {
        public void Configure(EntityTypeBuilder<SysTemHistory> builder)
        {
            builder.ToTable("tbl_SysTemHistorys");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.HistoryFlow)
                  .HasColumnType("nvarchar(max)");
        }
    }
}
