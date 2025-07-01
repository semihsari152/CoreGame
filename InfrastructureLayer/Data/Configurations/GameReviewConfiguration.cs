using DomainLayer.Entities.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Configurations
{
    public class GameReviewConfiguration : IEntityTypeConfiguration<GameReview>
    {
        public void Configure(EntityTypeBuilder<GameReview> builder)
        {
            builder.ToTable("GameReviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(x => x.Summary)
                .HasMaxLength(500);

            builder.Property(x => x.OverallRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.GameplayRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.GraphicsRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.SoundRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.StoryRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.ValueRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.DifficultyRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.Tags)
                .HasMaxLength(1000);

            builder.Property(x => x.Pros)
                .HasMaxLength(2000);

            builder.Property(x => x.Cons)
                .HasMaxLength(2000);

            builder.Property(x => x.MediaUrls)
                .HasMaxLength(2000);

            builder.Property(x => x.EditReason)
                .HasMaxLength(500);

            builder.Property(x => x.Slug)
                .HasMaxLength(250);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(300);

            // Indexes
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.GameId);
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.OverallRating);
            builder.HasIndex(x => x.CreatedDate);

            // Unique constraint: Bir kullanıcı bir oyuna sadece bir review yazabilir
            builder.HasIndex(x => new { x.UserId, x.GameId }).IsUnique();

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.GameReviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Game)
                .WithMany(x => x.GameReviews)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.HelpfulVotes)
                .WithOne(x => x.Review)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ReviewHistories)
                .WithOne(x => x.Review)
                .HasForeignKey(x => x.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
