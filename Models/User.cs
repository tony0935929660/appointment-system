using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppointmentSystem.Models;

public class User : IdentityUser
{
    public required string LineId { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int? MerchantId { get; set; }
    public Merchant? Merchant { get; set; }
}
