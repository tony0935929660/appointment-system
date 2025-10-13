namespace AppointmentSystem.Models;

public class Merchant
{
    public required int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public required string LineId { get; set; }
    public string CellPhone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<AvailableTime> AvailableTimes { get; set; } = new List<AvailableTime>();
}

