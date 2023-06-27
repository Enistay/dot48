using dot48.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dot48.Infra.Persistency.EntityTypeConfiguration
{
    public class ProfileConfiguration : EntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration()
        {
            ToTable("Profile");
            HasKey(k => k.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("IdProfile");

            Property(p => p.CodeProfile).HasMaxLength(50).IsRequired();

            Property(p => p.Description).HasMaxLength(100).IsRequired();

        }
    }
}
