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
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(500);

            builder.Property(x => x.Publisher)
                .HasMaxLength(100);

            builder.Property(x => x.Developer)
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.DiscountPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.AverageRating)
                .HasPrecision(18, 2);

            builder.Property(x => x.CoverImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.HeaderImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.BackgroundImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.TrailerUrl)
                .HasMaxLength(500);

            builder.Property(x => x.MetaTitle)
                .HasMaxLength(150);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(300);

            builder.Property(x => x.Tags)
                .HasMaxLength(1000);

            builder.Property(x => x.SteamAppId)
                .HasMaxLength(50);

            builder.Property(x => x.EpicStoreId)
                .HasMaxLength(50);

            builder.Property(x => x.IgdbId)
                .HasMaxLength(50);

            // Indexes
            builder.HasIndex(x => x.Title);
            builder.HasIndex(x => x.SteamAppId).IsUnique();
            builder.HasIndex(x => x.EpicStoreId).IsUnique();
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.ReleaseDate);

            // Relationships
            builder.HasMany(x => x.GameCategories)
              .WithOne(x => x.Game)
              .HasForeignKey(x => x.GameId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.GamePlatforms)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.GameTags)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.GameReviews)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.GameImages)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey("CommentableId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.UserGameLists)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
