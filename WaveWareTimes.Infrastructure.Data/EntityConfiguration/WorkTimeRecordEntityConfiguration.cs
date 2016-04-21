using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WaveWareTimes.Core.Entities.Domain;

namespace WaveWareTimes.Infrastructure.Data.EntityConfiguration
{
    public class WorkTimeRecordEntityConfiguration : EntityTypeConfiguration<WorkTimeRecord>
    {
        public WorkTimeRecordEntityConfiguration()
        {
            ToTable("WorkTimeRecords");
            HasKey(e => e.Id);
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId);

            Property(e => e.UserId).HasMaxLength(128).IsUnicode(false).IsRequired()
                    .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UserIdIndex") { IsUnique = false, Order = 2 }));

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(p => p.Start).HasColumnType("datetime").IsRequired();
            Property(p => p.End).HasColumnType("datetime").IsRequired();
            Property(e => e.Description).HasMaxLength(512).IsUnicode().IsRequired();

        }
    }
}
