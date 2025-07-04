using ApplicationLayer.DTOs.Games;
using AutoMapper;
using DomainLayer.Entities.Games;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mappings
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            // Game Entity ↔ DTOs
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Game, GameListDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                    src.GameCategories.Select(gc => gc.Category.Name).ToList()))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src =>
                    src.GamePlatforms.Select(gp => gp.Platform.DisplayName).ToList()));

            CreateMap<Game, GameDetailDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Screenshots, opt => opt.MapFrom(src =>
                    src.GameImages.Where(gi => gi.ImageType == GameImageType.Screenshot)
                        .Select(gi => gi.ImageUrl).ToList()))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.GameCategories))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.GamePlatforms))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Tags) ? new List<string>()
                    : src.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(t => t.Trim()).ToList()));

            CreateMap<GameCreateDto, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GameStatus.Draft))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.DownloadCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.GameCategories, opt => opt.Ignore())
                .ForMember(dest => dest.GamePlatforms, opt => opt.Ignore());

            CreateMap<GameUpdateDto, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Category mappings
            CreateMap<GameCategory, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Category.IconUrl))
                .ForMember(dest => dest.ColorCode, opt => opt.MapFrom(src => src.Category.ColorCode));

            CreateMap<Category, CategoryDto>();

            // Platform mappings
            CreateMap<GamePlatform, PlatformDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Platform.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Platform.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Platform.DisplayName))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Platform.IconUrl));

            CreateMap<DomainLayer.Entities.Games.Platform, PlatformDto>();
        }
    }
}
