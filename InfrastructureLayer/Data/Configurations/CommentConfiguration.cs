using DomainLayer.Entities.Forum;
using DomainLayer.Entities.Social;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.RelatedEntityTitle)
                .HasMaxLength(200);

            builder.Property(x => x.SenderName)
                .HasMaxLength(100);

            builder.Property(x => x.SenderAvatarUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ModerationReason)
                .HasMaxLength(500);

            builder.Property(x => x.AttachedMediaUrls)
                .HasMaxLength(2000);

            builder.Property(x => x.Mentions)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.ParentCommentId);
            builder.HasIndex(x => new { x.CommentableType, x.CommentableId });
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedDate);

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Self-referencing for nested comments
            builder.HasOne(x => x.ParentComment)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Likes)
                .WithOne()
                .HasForeignKey("LikeableId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Reports)
                .WithOne()
                .HasForeignKey("ReportableId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CommentHistories)
                .WithOne(x => x.Comment)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
