using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    v => v.Value,
                    v => Email.Create(v));

            builder.Property(b => b.Name)
                .HasConversion(
                    v => v.NameString,
                    v => Name.Create(v))
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.Password)
                .HasConversion(
                    v => v.PasswordString,
                    v => Password.Create(v))
                .IsRequired();

            builder.Property(b => b.PhoneNumber)
                .HasConversion(
                    v => v.Number,
                    v => PhoneNumber.Create(v))
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(b => b.TaxPayerNumber)
                .HasConversion(
                    v => v.Number,
                    v => TaxPayerNumber.Create(v))
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.Role)
                .HasConversion(
                    v => v.Value,
                    v => Role.Create(v))
                .IsRequired()
                .HasMaxLength(255);

        }
    }
}