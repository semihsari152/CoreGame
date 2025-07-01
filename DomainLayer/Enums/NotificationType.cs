using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum NotificationType
    {
        // Sosyal Etkileşimler
        CommentReply = 1,
        CommentLike = 2,
        ReviewLike = 3,
        PostLike = 4,
        NewFollower = 5,
        Mention = 6,

        // Forum Aktiviteleri
        TopicReply = 10,
        BestAnswerSelected = 11,
        TopicFollowUpdate = 12,

        // Oyun Güncellemeleri
        GamePriceDropped = 20,
        GameReleased = 21,
        GameUpdated = 22,

        // Achievement ve Gelişim
        AchievementUnlocked = 30,
        LevelUp = 31,
        BadgeEarned = 32,

        // Sistem Bildirimleri
        WelcomeMessage = 40,
        SystemMaintenance = 41,
        SecurityAlert = 42,
        PolicyUpdate = 43,

        // İçerik Moderasyonu
        ContentApproved = 50,
        ContentRejected = 51,
        AccountWarning = 52,

        // Pazarlama
        Newsletter = 60,
        SpecialOffer = 61,
        EventAnnouncement = 62
    }
}
