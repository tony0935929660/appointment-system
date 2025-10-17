namespace AppointmentSystem.Models;

public class Merchant
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? CellPhone { get; set; }
    public string? Address { get; set; }
    public string? Introduction { get; set; }
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<AvailableTime> AvailableTimes { get; set; } = new List<AvailableTime>();
}
