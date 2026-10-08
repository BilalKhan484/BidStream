using System.ComponentModel.DataAnnotations;
using BidStream.Domain.Common;
using BidStream.Domain.Enums;

namespace BidStream.Domain.Entities;

public class Item : BaseEntity
{
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public decimal StartingPrice { get; set; }
    public decimal CurrentPrice { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AuctionStatus Status { get; set; } = AuctionStatus.NotStarted;

    // Foreign Keys (Mapped to ASP.NET Identity Users later)
    public string SellerId { get; set; } = string.Empty;
    public string? WinnerId { get; set; }

    // Navigation Properties
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();

    // Optimistic Concurrency Token (Crucial for preventing double-bidding!)
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}