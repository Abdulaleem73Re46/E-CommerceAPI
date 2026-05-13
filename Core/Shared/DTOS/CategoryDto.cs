using Core.Entities;

namespace Core.Shared.DataTransferObjects;

public record CategoryDto
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Product?> Products { get; init; }
}

public record CategoryForCreationDto
{
    public string Name { get; init; } = string.Empty;
}

public record CategoryForUpdateDto
{
    public Guid CategoryId{get;init;}

    public string Name { get; init; }
}