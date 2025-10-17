using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSystem.Models;
using AppointmentSystem.Data;

namespace AppointmentSystem.Services;

public class MerchantService
{
    private readonly AppDbContext _dbContext;

    public MerchantService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateMerchantByUserAndNameAsync(User user, string name)
    {
        var merchant = new Merchant { Name = name };
        _dbContext.Merchants.Add(merchant);
        await _dbContext.SaveChangesAsync();

        user.MerchantId = merchant.Id;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
