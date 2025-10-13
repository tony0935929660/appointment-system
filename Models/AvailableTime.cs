using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Models;

public class AvailableTime
{
    public int Id { get; set; }
    public required int MerchantId { get; set; }
    public required DateTime Date { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required Merchant Merchant { get; set; }
}