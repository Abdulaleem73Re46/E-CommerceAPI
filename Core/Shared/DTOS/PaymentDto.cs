using Core.Enum;
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;

namespace Core.Shared.DataTransferObjects;

public record PaymentDto
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public PaymentStatus Status { get; init; }
    public DateTime PayDate { get; init; }
}

public record PaymentForCreationDto
{
    public PaymentMethod PaymentMethod { get; init; }
}