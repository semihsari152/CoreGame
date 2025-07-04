using ApplicationLayer.DTOs.Users.CoreGame.Application.DTOs.Users;
using AutoMapper;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // User Entity ↔ DTOs
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<User, UserListDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(src =>
                    src.LastActivityDate.HasValue && src.LastActivityDate.Value > DateTime.UtcNow.AddMinutes(-15)));

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Achievements, opt => opt.MapFrom(src => src.UserAchievements))
                .ForMember(dest => dest.TotalGamesPlayed, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.TotalGamesPlayed : 0))
                .ForMember(dest => dest.TotalHoursPlayed, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.TotalHoursPlayed : 0))
                .ForMember(dest => dest.TotalReviewsWritten, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.TotalReviewsWritten : 0))
                .ForMember(dest => dest.TotalCommentsPosted, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.TotalCommentsPosted : 0))
                .ForMember(dest => dest.TotalGuidesCreated, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.TotalGuidesCreated : 0))
                .ForMember(dest => dest.ActivityScore, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.ActivityScore : 0))
                .ForMember(dest => dest.ConsecutiveDays, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.ConsecutiveDays : 0))
                .ForMember(dest => dest.LastStreakDate, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.LastStreakDate : null))
                .ForMember(dest => dest.PreferredDifficulty, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.PreferredDifficulty : null))
                .ForMember(dest => dest.PrefersSinglePlayer, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.PrefersSinglePlayer : true))
                .ForMember(dest => dest.PrefersMultiplayer, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.PrefersMultiplayer : true))
                .ForMember(dest => dest.PrefersCompetitive, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.PrefersCompetitive : false))
                .ForMember(dest => dest.PrefersCoop, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.PrefersCoop : true));

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => UserStatus.PendingVerification))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TotalPoints, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.ExperiencePoints, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // This will be handled separately

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserProfileUpdateDto, UserProfile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // UserAchievement mappings
            CreateMap<UserAchievement, UserAchievementDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Achievement.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Achievement.Description))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Achievement.IconUrl))
                .ForMember(dest => dest.BadgeUrl, opt => opt.MapFrom(src => src.Achievement.BadgeUrl))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Achievement.Type.ToString()))
                .ForMember(dest => dest.Rarity, opt => opt.MapFrom(src => src.Achievement.Rarity.ToString()))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Achievement.Points));

            // Achievement mapping
            CreateMap<Achievement, UserAchievementDto>()
                .ForMember(dest => dest.AchievementId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Rarity, opt => opt.MapFrom(src => src.Rarity.ToString()))
                .ForMember(dest => dest.EarnedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDisplayed, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.EarnedDescription, opt => opt.Ignore());
        }
    }
}
