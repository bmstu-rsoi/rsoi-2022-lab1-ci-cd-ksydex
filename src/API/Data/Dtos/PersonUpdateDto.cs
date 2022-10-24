namespace API.Data.Dtos;

public record PersonUpdateDto
{
    public string? Name { get; set; } = "";
    public string? Address { get; set; } = "";
    public int? Age { get; set; }
}