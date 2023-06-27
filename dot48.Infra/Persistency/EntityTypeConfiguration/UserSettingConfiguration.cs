using dot48.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace dot48.Infra.Persistency.EntityTypeConfiguration
{
    public  class UserSettingConfiguration : EntityTypeConfiguration<UserSetting>
    {
        public UserSettingConfiguration()
        {
            ToTable("UserSetting");
            HasKey(k => k.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("IdUserSetting");

            Property(p => p.MO).IsOptional();

            Property(p => p.RecipientMO).IsOptional();

            Property(p => p.DIR).IsOptional();

            Property(p => p.RecipientDIR).IsOptional();

            Property(p => p.WorkHours).IsOptional();
        }
    }
}
