using System.ComponentModel.DataAnnotations.Schema;

namespace TheCrudApp.Database.Entities;

[Table("cars")]
public class Car
{
    [Column("id")]
    public required Guid Id { get; set; }
    [Column("model")]
    public string? Model { get; set; }
    [Column("year")]
    public int? Year { get; set; }
    [Column("price")]
    public decimal? Price { get; set; }
}