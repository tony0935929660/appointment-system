using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Dtos.Requests.Auth;

public class LoginRequest
{
    public required string LineAccessToken { get; set; }
    public required UserType Type { get; set; }
}

public enum UserType
{
    Merchant,
    Customer
}
