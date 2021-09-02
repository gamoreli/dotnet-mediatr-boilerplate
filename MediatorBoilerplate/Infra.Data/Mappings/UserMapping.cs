using MediatorBoilerplate.Domain.Models;
using MediatorBoilerplate.Infra.Data.Infra;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatorBoilerplate.Infra.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.Property(user => user.Email)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}