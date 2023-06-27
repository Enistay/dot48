using dot48.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace dot48.Infra.Persistency.EntityTypeConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(k => k.Id)
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("IdUser");

            Property(p => p.Name).HasMaxLength(150).IsRequired();

            Property(p => p.AliasName).HasMaxLength(150).IsOptional();

            Property(p => p.Nif).HasMaxLength(20).IsOptional();

            Property(p => p.CodeUser).HasMaxLength(150).IsRequired()
                                   .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                      new IndexAnnotation(new IndexAttribute("IX_CodeUser", 1) { IsUnique = true }));


            Property(p => p.Email).HasMaxLength(150).IsRequired()
                                     .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                        new IndexAnnotation(new IndexAttribute("IX_Email", 2) { IsUnique = true }));

            Property(p => p.PhoneNumber).HasMaxLength(14).IsOptional();

            Property(p => p.Password).HasMaxLength(64).IsRequired();

            Property(p => p.Enable).IsRequired();

            Property(p => p.CreateDate).IsRequired();

            Property(p => p.UpdateDate).IsOptional();

        }
    }
}
