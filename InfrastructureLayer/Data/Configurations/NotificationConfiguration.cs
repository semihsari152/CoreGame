using DomainLayer.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.ActionUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.IconUrl)
                .HasMaxLength(500);

            builder.Property(x => x.RelatedEntityType)
                .HasMaxLength(100);

            builder.Property(x => x.RelatedEntityTitle)
                .HasMaxLength(200);

            builder.Property(x => x.SenderName)
                .HasMaxLength(100);

            builder.Property(x => x.SenderAvatarUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Category)
                .HasMaxLength(100);

            builder.Property(x => x.GroupKey)
                .HasMaxLength(100);

            builder.Property(x => x.Metadata)
                .HasMaxLength(2000);

            builder.Property(x => x.Tags)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.IsRead);
            builder.HasIndex(x => x.CreatedDate);
            builder.HasIndex(x => x.GroupKey);

            // DÜZELT (yeni):
            builder.HasOne(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(x => x.NotificationActions)
                .WithOne(x => x.Notification)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
