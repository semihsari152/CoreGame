using DomainLayer.Common;
using DomainLayer.Entities.Content;
using DomainLayer.Entities.Forum;
using DomainLayer.Entities.Games;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.System;
using DomainLayer.Entities.Users;
using InfrastructureLayer.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Context
{
    public class CoreGameDbContext : IdentityDbContext<ApplicationUser>
    {
        public CoreGameDbContext(DbContextOptions<CoreGameDbContext> options) : base(options)
        {
        }

        #region Game Entities
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<ReviewHelpful> ReviewHelpfuls { get; set; }
        public DbSet<ReviewHistory> ReviewHistories { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<ReviewAward> ReviewAwards { get; set; }
        #endregion

        #region User Entities
        public DbSet<User> AppUsers { get; set; } // Identity User ile karışmasın diye
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserGameList> UserGameLists { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        #endregion

        #region Social Entities
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<CommentHistory> CommentHistories { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        #endregion

        #region Forum Entities
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<ForumTopic> ForumTopics { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<PostHelpful> PostHelpfuls { get; set; }
        public DbSet<PostHistory> PostHistories { get; set; }
        public DbSet<TopicFollow> TopicFollows { get; set; }
        public DbSet<ForumModerator> ForumModerators { get; set; }
        #endregion

        #region Content Entities
        public DbSet<Guide> Guides { get; set; }
        public DbSet<GuideStep> GuideSteps { get; set; }
        public DbSet<GuideRating> GuideRatings { get; set; }
        public DbSet<GuideBookmark> GuideBookmarks { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Media> MediaFiles { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        #endregion

        #region System Entities
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationAction> NotificationActions { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<NotificationTemplateAction> NotificationTemplateActions { get; set; }
        public DbSet<NotificationPreference> NotificationPreferences { get; set; }
        public DbSet<NotificationQueue> NotificationQueues { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreGameDbContext).Assembly);

            // Global filters
            ApplyGlobalFilters(modelBuilder);

            // Seed data
            SeedData(modelBuilder);
        }

        private void ApplyGlobalFilters(ModelBuilder modelBuilder)
        {
            // Soft delete filter for all ISoftDeletable entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(CoreGameDbContext)
                        .GetMethod(nameof(GetSoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        ?.MakeGenericMethod(entityType.ClrType);

                    var filter = method?.Invoke(null, Array.Empty<object>());
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter((System.Linq.Expressions.LambdaExpression)filter!);
                }
            }
        }

        private static System.Linq.Expressions.LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDeletable
        {
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed initial data here
            SeedCategories(modelBuilder);
            SeedPlatforms(modelBuilder);
            SeedAchievements(modelBuilder);
        }

        private void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", Description = "Action games", ColorCode = "#FF6B6B", DisplayOrder = 1, CreatedDate = DateTime.UtcNow },
                new Category { Id = 2, Name = "Adventure", Description = "Adventure games", ColorCode = "#4ECDC4", DisplayOrder = 2, CreatedDate = DateTime.UtcNow },
                new Category { Id = 3, Name = "RPG", Description = "Role-playing games", ColorCode = "#45B7D1", DisplayOrder = 3, CreatedDate = DateTime.UtcNow },
                new Category { Id = 4, Name = "Strategy", Description = "Strategy games", ColorCode = "#96CEB4", DisplayOrder = 4, CreatedDate = DateTime.UtcNow },
                new Category { Id = 5, Name = "Sports", Description = "Sports games", ColorCode = "#FFEAA7", DisplayOrder = 5, CreatedDate = DateTime.UtcNow },
                new Category { Id = 6, Name = "Racing", Description = "Racing games", ColorCode = "#DDA0DD", DisplayOrder = 6, CreatedDate = DateTime.UtcNow },
                new Category { Id = 7, Name = "Puzzle", Description = "Puzzle games", ColorCode = "#98D8C8", DisplayOrder = 7, CreatedDate = DateTime.UtcNow },
                new Category { Id = 8, Name = "Simulation", Description = "Simulation games", ColorCode = "#F7DC6F", DisplayOrder = 8, CreatedDate = DateTime.UtcNow }
            );
        }

        private void SeedPlatforms(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().HasData(
                new Platform { Id = 1, Name = "Steam", DisplayName = "Steam", IconUrl = "/images/platforms/steam.png", WebsiteUrl = "https://store.steampowered.com", DisplayOrder = 1, CreatedDate = DateTime.UtcNow },
                new Platform { Id = 2, Name = "Epic", DisplayName = "Epic Games Store", IconUrl = "/images/platforms/epic.png", WebsiteUrl = "https://www.epicgames.com/store", DisplayOrder = 2, CreatedDate = DateTime.UtcNow },
                new Platform { Id = 3, Name = "PlayStation", DisplayName = "PlayStation Store", IconUrl = "/images/platforms/playstation.png", WebsiteUrl = "https://store.playstation.com", DisplayOrder = 3, CreatedDate = DateTime.UtcNow },
                new Platform { Id = 4, Name = "Xbox", DisplayName = "Microsoft Store", IconUrl = "/images/platforms/xbox.png", WebsiteUrl = "https://www.microsoft.com/store/games/xbox", DisplayOrder = 4, CreatedDate = DateTime.UtcNow },
                new Platform { Id = 5, Name = "Nintendo", DisplayName = "Nintendo eShop", IconUrl = "/images/platforms/nintendo.png", WebsiteUrl = "https://www.nintendo.com/games", DisplayOrder = 5, CreatedDate = DateTime.UtcNow },
                new Platform { Id = 6, Name = "GOG", DisplayName = "GOG.com", IconUrl = "/images/platforms/gog.png", WebsiteUrl = "https://www.gog.com", DisplayOrder = 6, CreatedDate = DateTime.UtcNow }
            );
        }

        private void SeedAchievements(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>().HasData(
                new Achievement { Id = 1, Name = "First Steps", Description = "Register and complete your profile", IconUrl = "/images/achievements/first-steps.png", Type = DomainLayer.Enums.AchievementType.Special, Rarity = DomainLayer.Enums.AchievementRarity.Common, Points = 10, RequiredValue = 1, Category = "Getting Started", CreatedDate = DateTime.UtcNow },
                new Achievement { Id = 2, Name = "Reviewer", Description = "Write your first game review", IconUrl = "/images/achievements/reviewer.png", Type = DomainLayer.Enums.AchievementType.ReviewCount, Rarity = DomainLayer.Enums.AchievementRarity.Common, Points = 25, RequiredValue = 1, Category = "Content Creation", CreatedDate = DateTime.UtcNow },
                new Achievement { Id = 3, Name = "Active Commenter", Description = "Post 10 comments", IconUrl = "/images/achievements/commenter.png", Type = DomainLayer.Enums.AchievementType.CommentCount, Rarity = DomainLayer.Enums.AchievementRarity.Uncommon, Points = 50, RequiredValue = 10, Category = "Community", CreatedDate = DateTime.UtcNow },
                new Achievement { Id = 4, Name = "Dedication", Description = "Log in for 7 consecutive days", IconUrl = "/images/achievements/dedication.png", Type = DomainLayer.Enums.AchievementType.LoginStreak, Rarity = DomainLayer.Enums.AchievementRarity.Rare, Points = 100, RequiredValue = 7, Category = "Activity", CreatedDate = DateTime.UtcNow },
                new Achievement { Id = 5, Name = "Guide Master", Description = "Create 5 guides", IconUrl = "/images/achievements/guide-master.png", Type = DomainLayer.Enums.AchievementType.GuideCreated, Rarity = DomainLayer.Enums.AchievementRarity.Epic, Points = 200, RequiredValue = 5, Category = "Content Creation", CreatedDate = DateTime.UtcNow }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Audit fields için otomatik doldurma
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        // TODO: Get current user from HttpContext
                        // entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        // entry.Entity.UpdatedBy = _currentUserService.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
