using Core.Entities;
using Core.Enum;
using Core.Enum.OrderStatus;

namespace Core.Shared.DataTransferObjects;

public record OrderDto
{
    public Guid OrderId { get; init; }
    public string UserId { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public ICollection<OrderItemDto> OrderItems { get; init; }
}

public record OrderForCreationDto
{
    public decimal TotalPrice { get; init; }
    public ICollection<OrderItemForCreationDto> OrderItems { get; init; }
}