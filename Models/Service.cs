namespace AppointmentSystem.Models;

public class Service
{
    public int Id { get; set; }
    public required int MerchantId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public required int Price { get; set; }
    public required int Duration { get; set; }
    public Merchant Merchant { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
