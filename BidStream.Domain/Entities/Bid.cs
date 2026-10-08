using BidStream.Domain.Common;

namespace BidStream.Domain.Entities;

public class Bid : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime BidTime { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public string BidderId { get; set; } = string.Empty;
}