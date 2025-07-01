using DomainLayer.Common;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class ContentCategory : BaseEntity
    {
        // Temel Bilgiler
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string ColorCode { get; set; } = "#007bff";

        // Kategori Türü
        public ContentCategoryType Type { get; set; }

        // Hiyerarşi
        public int? ParentCategoryId { get; set; }
        public int Level { get; set; } = 0;
        public string? Path { get; set; }

        // Görünüm
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;

        // İstatistikler
        public int ContentCount { get; set; } = 0;

        // SEO
        public string? Slug { get; set; }
        public string? MetaDescription { get; set; }

        // Navigation Properties
        public virtual ContentCategory? ParentCategory { get; set; }
        public virtual ICollection<ContentCategory> SubCategories { get; set; } = new List<ContentCategory>();
    }
}
