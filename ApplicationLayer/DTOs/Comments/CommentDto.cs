using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string AuthorUsername { get; set; } = string.Empty;
        public string? AuthorAvatarUrl { get; set; }
        public int? ParentCommentId { get; set; }
        public int Level { get; set; }
        public string CommentableType { get; set; } = string.Empty;
        public int CommentableId { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int NetScore { get; set; }
        public int ReplyCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsEdited { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsPinned { get; set; }
        public bool IsSpoiler { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentDto> Replies { get; set; } = new();
    }

    public class CommentListDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorUsername { get; set; } = string.Empty;
        public string? AuthorAvatarUrl { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int NetScore { get; set; }
        public int ReplyCount { get; set; }
        public bool IsPinned { get; set; }
        public bool IsSpoiler { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? RelatedEntityTitle { get; set; }
    }

    public class CommentCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public int? ParentCommentId { get; set; }
        public string CommentableType { get; set; } = string.Empty;
        public int CommentableId { get; set; }
        public bool IsSpoiler { get; set; }
    }

    public class CommentUpdateDto
    {
        public string Content { get; set; } = string.Empty;
        public bool IsSpoiler { get; set; }
    }
}
