using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Models;

public class Appointment
{
    public required int MerchantId { get; set; }
    public required int CustomerId { get; set; }
    public required int ServiceId { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required int Price { get; set; }
    public required int Deposit { get; set; }
    public bool? IsDepositPaid { get; set; }
    public required Merchant Merchant { get; set; }
    public required Customer Customer { get; set; }
    public required Service Service { get; set; }
}
