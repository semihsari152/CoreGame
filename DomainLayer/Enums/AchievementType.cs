using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum AchievementType
    {
        ReviewCount = 0,      // X kadar review yaz
        CommentCount = 1,     // X kadar yorum yap
        LoginStreak = 2,      // X gün üst üste giriş
        GameCount = 3,        // X kadar oyun ekle
        LikeReceived = 4,     // X kadar beğeni al
        GuideCreated = 5,     // X kadar rehber oluştur
        ForumPost = 6,        // X kadar forum gönderisi
        Follower = 7,         // X kadar takipçin olsun
        Special = 8,          // Özel etkinlik
        Anniversary = 9       // Yıldönümü
    }
}
