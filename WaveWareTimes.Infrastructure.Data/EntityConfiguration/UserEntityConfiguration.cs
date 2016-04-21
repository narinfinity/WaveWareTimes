using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WaveWareTimes.Core.Entities.Domain;

namespace WaveWareTimes.Infrastructure.Data.EntityConfiguration
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("Users");
            HasKey(e => e.Id);
            HasMany(e => e.Claims);

            Property(e => e.Id).HasMaxLength(128).IsUnicode(false).IsRequired();
            Property(e => e.FirstName).HasMaxLength(100).IsUnicode().IsOptional();
            Property(e => e.LastName).HasMaxLength(100).IsUnicode().IsOptional();
            Property(e => e.Email).HasMaxLength(100).IsUnicode(false).IsOptional();
            Property(e => e.UserName).HasMaxLength(100).IsUnicode().IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 1 }));
            Property(e => e.PasswordHash).HasMaxLength(256).IsUnicode(false).IsOptional();
            Property(e => e.SecurityStamp).HasMaxLength(256).IsUnicode(false).IsOptional();
            Property(e => e.PhoneNumber).HasMaxLength(100).IsUnicode(false).IsOptional();

        }
    }
}
