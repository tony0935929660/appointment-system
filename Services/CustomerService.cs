using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Models;
using AppointmentSystem.Data;

namespace AppointmentSystem.Services;

public class CustomerService
{
    private readonly AppDbContext _dbContext;

    public CustomerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateCustomerByUserAndNameAsync(User user, string name)
    {
        var customer = new Customer { Name = name };
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        user.CustomerId = customer.Id;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
