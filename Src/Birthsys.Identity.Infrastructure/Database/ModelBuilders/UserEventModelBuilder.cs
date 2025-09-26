using Birthsys.Identity.Domain.Aggregates.UserEvents;
using Birthsys.Identity.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Birthsys.Identity.Infrastructure.Database.ModelBuilders
{
    internal sealed class UserEventModelBuilder : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.ToTable("user_events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasConversion(
                    id => id!.Value,
                    value => UserEventId.FromGuid(value));

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired()
                .HasConversion(
                    userId => userId!.Value,
                    value => UserId.FromGuid(value));

            builder.Property(x => x.Process)
                .HasColumnName("process")
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(
                    process => process!.Code,
                    value => UserEventProcess.FromCode(value));

            builder.Property(x => x.OccurredAt)
                .HasColumnName("occurred_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(
                    status => status!.Code,
                    value => UserEventStatus.FromCode(value));

            builder.Property(x => x.ProcessDetails)
                .HasColumnName("process_details")
                .IsRequired(false);

            builder.Property(x => x.EventDetails)
                .HasColumnName("event_details")
                .IsRequired(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}