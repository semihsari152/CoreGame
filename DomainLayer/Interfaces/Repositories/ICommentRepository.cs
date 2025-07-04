using DomainLayer.Entities.Social;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        // Comment-specific methods
        Task<List<Comment>> GetCommentsByEntityAsync(CommentableType entityType, int entityId);
        Task<List<Comment>> GetCommentRepliesAsync(int parentCommentId);
        Task<List<Comment>> GetUserCommentsAsync(int userId, int pageNumber = 1, int pageSize = 20);
        Task<List<Comment>> GetTopCommentsAsync(CommentableType entityType, int entityId, int count = 10);
        Task<List<Comment>> GetRecentCommentsAsync(int count = 50);
        Task<List<Comment>> GetCommentsForModerationAsync();
        Task<Comment?> GetCommentWithRepliesAsync(int commentId);
        Task<int> GetCommentCountByEntityAsync(CommentableType entityType, int entityId);
        Task<Dictionary<int, int>> GetCommentCountsAsync(CommentableType entityType, List<int> entityIds);
        Task UpdateCommentLikesAsync(int commentId, int likeCount, int dislikeCount);
        Task IncrementReplyCountAsync(int parentCommentId);
        Task DecrementReplyCountAsync(int parentCommentId);
    }
}
