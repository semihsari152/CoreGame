using ApplicationLayer.DTOs.Comments;
using AutoMapper;
using DomainLayer.Entities.Social;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mappings
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            // Comment Entity ↔ DTOs
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.AuthorAvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CommentableType, opt => opt.MapFrom(src => src.CommentableType.ToString()))
                .ForMember(dest => dest.NetScore, opt => opt.MapFrom(src => src.LikeCount - src.DislikeCount))
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies));

            CreateMap<Comment, CommentListDto>()
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.AuthorAvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.NetScore, opt => opt.MapFrom(src => src.LikeCount - src.DislikeCount))
                .ForMember(dest => dest.RelatedEntityTitle, opt => opt.MapFrom(src => src.RelatedEntityTitle));

            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => CommentStatus.Published))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.DislikeCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.ReplyCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.ReportCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => 0));

            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CommentableType, opt => opt.Ignore())
                .ForMember(dest => dest.CommentableId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsEdited, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.EditedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
