namespace BidStream.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; } // Will map to Identity User ID later
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}