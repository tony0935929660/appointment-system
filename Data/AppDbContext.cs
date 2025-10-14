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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Merchant>().HasKey(m => m.Id);

        modelBuilder.Entity<Customer>().HasKey(c => c.Id);

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasOne(s => s.Merchant)
                .WithMany(m => m.Services) // 假設 Merchant Model 中有 Services 集合
                .HasForeignKey(s => s.MerchantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // 解決連鎖刪除問題
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne(a => a.Merchant)
                .WithMany(m => m.Appointments) // 假設 Merchant Model 中有 Appointments 集合
                .HasForeignKey(a => a.MerchantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // 避免多重連鎖刪除

            entity.HasOne(a => a.Customer)
                .WithMany(c => c.Appointments) // 假設 Customer Model 中有 Appointments 集合
                .HasForeignKey(a => a.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // 避免多重連鎖刪除

            entity.HasOne(a => a.Service)
                .WithMany(s => s.Appointments) // 假設 Service Model 中有 Appointments 集合
                .HasForeignKey(a => a.ServiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // 避免多重連鎖刪除
        });

        modelBuilder.Entity<AvailableTime>(entity =>
        {
            entity.HasOne(at => at.Merchant)
                .WithMany(m => m.AvailableTimes)
                .HasForeignKey(at => at.MerchantId) 
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 
        });
        
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
}

