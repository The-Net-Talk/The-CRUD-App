namespace TheCrudApp.Models.Dto;

public class CarDto
{
    public required Guid Id { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? Price { get; set; }
}