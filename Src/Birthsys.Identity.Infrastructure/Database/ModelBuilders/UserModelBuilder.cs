using Birthsys.Identity.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Birthsys.Identity.Infrastructure.Database.ModelBuilders
{
    internal sealed class UserModelBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasConversion(
                    id => id!.Value,
                    value => UserId.FromGuid(value));

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(256)
                .HasConversion(
                    email => email!.Value,
                    value => new UserEmail(value));

            builder.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired()
                .HasMaxLength(512)
                .HasConversion(
                    hash => hash!.Value,
                    value => new UserPasswordHash(value));
            
            builder.Property(x => x.PasswordSalt)
                .HasColumnName("password_salt")
                .IsRequired()
                .HasMaxLength(256)
                .HasConversion(
                    salt => salt!.Value,
                    value => new UserPasswordSalt(value));

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.Property(x => x.DeleteAt)
                .HasColumnName("delete_at")
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(
                    name => name!.Value,
                    value => new UserName(value));

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(
                    lastName => lastName!.Value,
                    value => new UserLastName(value));

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("date_of_birth")
                .IsRequired()
                .HasConversion(
                    date => date!.Value,
                    value => UserDateBirth.FromDateTime(value));

            builder.HasIndex(x => x.Email)
                .IsUnique();

        }
    }
}