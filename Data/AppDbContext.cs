using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Models;

namespace AppointmentSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<AvailableTime> AvailableTimes { get; set; }
}

