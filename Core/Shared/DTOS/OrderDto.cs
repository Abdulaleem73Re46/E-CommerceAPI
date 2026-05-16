using Core.Entities;
using Core.Enum;
using Core.Enum.OrderStatus;

namespace Core.Shared.DataTransferObjects;

// public record OrderDto
// {
//     public Guid OrderId { get; init; }
//     public string UserId { get; init; }
//     public decimal TotalPrice { get; init; }
//     public DateTime OrderDate { get; init; }
//     public OrderStatus Status { get; init; }
//     public ICollection<OrderItemDto> OrderItems { get; init; }
// }

// public record OrderForCreationDto
// {

   
//     public string UserId { get; init; }
//     public decimal TotalPrice { get; init; }
//     public ICollection<OrderItemForCreationDto> OrderItems { get; init; }

// }

public record OrderForCreationDto
{
    //public decimal TotalPrice { get; init; }
    public ICollection<OrderItemForCreationDto> OrderItems { get; init; } = new List<OrderItemForCreationDto>();
}

public record OrderItemForCreationDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}

public record OrderDto
{
    public Guid OrderId { get; init; }
    public string UserId { get; init; } = string.Empty;
    public decimal TotalPrice { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public ICollection<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
}

public record OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}