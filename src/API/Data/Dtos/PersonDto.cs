namespace API.Data.Dtos;

public record PersonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public int Age { get; set; }
}