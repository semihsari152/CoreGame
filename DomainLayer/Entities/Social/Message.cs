using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Social
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }

        // Mesaj İçeriği
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public MessageType Type { get; set; } = MessageType.Private;

        // Durum
        public bool IsRead { get; set; } = false;
        public DateTime? ReadDate { get; set; }
        public bool IsDeletedBySender { get; set; } = false;
        public bool IsDeletedByRecipient { get; set; } = false;

        // Thread System
        public int? ParentMessageId { get; set; }
        public string? ThreadId { get; set; } // Conversation grouping

        // Ek Özellikler
        public string? AttachmentUrls { get; set; } // JSON array
        public MessagePriority Priority { get; set; } = MessagePriority.Normal;

        // Navigation Properties
        public virtual User Sender { get; set; } = null!;
        public virtual User Recipient { get; set; } = null!;
        public virtual Message? ParentMessage { get; set; }
        public virtual ICollection<Message> Replies { get; set; } = new List<Message>();
    }
}
