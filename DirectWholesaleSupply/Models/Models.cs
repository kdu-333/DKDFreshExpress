using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectWholesaleSupply.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } = "";

        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role? Role { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = "";

        [Required, MaxLength(50)]
        public string Username { get; set; } = "";

        [Required, MaxLength(100)]
        public string Password { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; } = "";

        public int SortOrder { get; set; } = 0;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = "";

        [Required, MaxLength(20)]
        public string Unit { get; set; } = "kg"; // kg or pc

        [Column(TypeName = "decimal(10,2)")]
        public decimal WholesalePrice { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal RetailPrice { get; set; } = 0;

        [MaxLength(255)]
        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public int SortOrder { get; set; } = 0;

        public ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
    }

    public class PriceHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User? User { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal OldWholesalePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal NewWholesalePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal OldRetailPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal NewRetailPrice { get; set; }

        public DateTime ChangedAt { get; set; }
    }

    public class AuditLog
    {
        [Key]
        public int LogID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User? User { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; } = "";

        [MaxLength(500)]
        public string? Details { get; set; }

        public DateTime LogDate { get; set; }
    }
}
