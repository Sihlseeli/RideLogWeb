using System.Text.Json.Serialization;

namespace RideLogWeb.Models;

public enum BikeType
{
    Gravel,
    MTB
}

public class Activity
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public string Title { get; set; } = string.Empty;
    public BikeType BikeType { get; set; } = BikeType.Gravel;
    public double Kilometers { get; set; }
    public int ElevationMeters { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Note { get; set; }
    public bool IsTemplate { get; set; } = false;
}