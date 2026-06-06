using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UserPermissions")]
public class UserPermission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string PermissionName { get; set; } = null!; // مثل "Product.Create"

    // معلومات إضافية (اختيارية)
    public string? GrantedBy { get; set; }   // من أعطى الإذن
    public DateTime GrantedDate { get; set; } = DateTime.UtcNow;

    // علاقة مع User (إذا أردت التنقل)
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
}