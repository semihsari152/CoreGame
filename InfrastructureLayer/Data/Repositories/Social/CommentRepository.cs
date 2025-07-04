using DomainLayer.Entities.Social;
using DomainLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.Data.Context;
using InfrastructureLayer.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Repositories.Social
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(CoreGameDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByEntityAsync(CommentableType entityType, int entityId)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .Where(c => c.CommentableType == entityType &&
                           c.CommentableId == entityId &&
                           c.ParentCommentId == null &&
                           c.Status == CommentStatus.Published)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentRepliesAsync(int parentCommentId)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.ParentCommentId == parentCommentId && c.Status == CommentStatus.Published)
                .OrderBy(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetUserCommentsAsync(int userId, int pageNumber = 1, int pageSize = 20)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.UserId == userId && c.Status == CommentStatus.Published)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetTopCommentsAsync(CommentableType entityType, int entityId, int count = 10)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.CommentableType == entityType &&
                           c.CommentableId == entityId &&
                           c.Status == CommentStatus.Published)
                .OrderByDescending(c => c.LikeCount - c.DislikeCount)
                .ThenByDescending(c => c.LikeCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetRecentCommentsAsync(int count = 50)
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.Status == CommentStatus.Published)
                .OrderByDescending(c => c.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsForModerationAsync()
        {
            return await _dbSet
                .Include(c => c.User)
                .Where(c => c.Status == CommentStatus.UnderReview || c.ReportCount > 0)
                .OrderByDescending(c => c.ReportCount)
                .ThenByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Comment?> GetCommentWithRepliesAsync(int commentId)
        {
            return await _dbSet
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<int> GetCommentCountByEntityAsync(CommentableType entityType, int entityId)
        {
            return await _dbSet
                .CountAsync(c => c.CommentableType == entityType &&
                               c.CommentableId == entityId &&
                               c.Status == CommentStatus.Published);
        }

        public async Task<Dictionary<int, int>> GetCommentCountsAsync(CommentableType entityType, List<int> entityIds)
        {
            return await _dbSet
                .Where(c => c.CommentableType == entityType &&
                           entityIds.Contains(c.CommentableId) &&
                           c.Status == CommentStatus.Published)
                .GroupBy(c => c.CommentableId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task UpdateCommentLikesAsync(int commentId, int likeCount, int dislikeCount)
        {
            var comment = await GetByIdAsync(commentId);
            if (comment != null)
            {
                comment.LikeCount = likeCount;
                comment.DislikeCount = dislikeCount;
                Update(comment);
            }
        }

        public async Task IncrementReplyCountAsync(int parentCommentId)
        {
            var comment = await GetByIdAsync(parentCommentId);
            if (comment != null)
            {
                comment.ReplyCount++;
                Update(comment);
            }
        }

        public async Task DecrementReplyCountAsync(int parentCommentId)
        {
            var comment = await GetByIdAsync(parentCommentId);
            if (comment != null && comment.ReplyCount > 0)
            {
                comment.ReplyCount--;
                Update(comment);
            }
        }
    }
}
