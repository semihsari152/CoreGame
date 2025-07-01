using DomainLayer.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Configurations
{
    public class ForumCategoryConfiguration : IEntityTypeConfiguration<ForumCategory>
    {
        public void Configure(EntityTypeBuilder<ForumCategory> builder)
        {
            builder.ToTable("ForumCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IconUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ColorCode)
                .HasMaxLength(10);

            builder.Property(x => x.Path)
                .HasMaxLength(500);

            builder.Property(x => x.AllowedRoles)
                .HasMaxLength(1000);

            builder.Property(x => x.PostPermissions)
                .HasMaxLength(1000);

            builder.Property(x => x.ModeratorIds)
                .HasMaxLength(1000);

            builder.Property(x => x.Slug)
                .HasMaxLength(150);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(300);

            // Indexes
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.HasIndex(x => x.ParentCategoryId);
            builder.HasIndex(x => x.DisplayOrder);

            // Self-referencing relationship
            builder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Topics)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ForumTopicConfiguration : IEntityTypeConfiguration<ForumTopic>
    {
        public void Configure(EntityTypeBuilder<ForumTopic> builder)
        {
            builder.ToTable("ForumTopics");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(x => x.Tags)
                .HasMaxLength(1000);

            builder.Property(x => x.AttachedFiles)
                .HasMaxLength(2000);

            builder.Property(x => x.RelatedGameIds)
                .HasMaxLength(500);

            builder.Property(x => x.Slug)
                .HasMaxLength(250);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(300);

            builder.Property(x => x.ModerationNotes)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.CreatedDate);

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Topic)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Followers)
                .WithOne(x => x.Topic)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.ToTable("ForumPosts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(x => x.AttachedFiles)
                .HasMaxLength(2000);

            builder.Property(x => x.QuotedPostIds)
                .HasMaxLength(500);

            builder.Property(x => x.EditReason)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.TopicId);
            builder.HasIndex(x => x.ParentPostId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedDate);

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Topic)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            // Self-referencing for nested posts
            builder.HasOne(x => x.ParentPost)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.ParentPostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.HelpfulVotes)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}