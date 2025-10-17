using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AppointmentSystem.Models;
using AppointmentSystem.Data;
using AppointmentSystem.Services;
using AppointmentSystem.Dtos.Requests.Auth;

namespace AppointmentSystem.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _dbContext;
    private readonly JwtService _jwtService;
    private readonly LineService _lineService;
    private readonly CustomerService _customerService;
    private readonly MerchantService _merchantService;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        AppDbContext dbContext,
        JwtService jwtService,
        LineService lineService,
        CustomerService customerService,
        MerchantService merchantService
    )
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _jwtService = jwtService;
        _lineService = lineService;
        _customerService = customerService;
        _merchantService = merchantService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var lineProfile = await _lineService.GetLineProfileAsync(request.LineAccessToken);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.LineId == lineProfile.UserId);
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            if (user != null)
            {
                if (request.Type == UserType.Merchant && user.MerchantId == null)
                {
                    await _merchantService.CreateMerchantByUserAndNameAsync(user, lineProfile.DisplayName);
                    
                }
                else if (request.Type == UserType.Customer && user.CustomerId == null)
                {
                    await _customerService.CreateCustomerByUserAndNameAsync(user, lineProfile.DisplayName);
                }

                await transaction.CommitAsync();
                return Ok(new { Token = _jwtService.GenerateJwtToken(user) });
            }

            var newUser = new User
            {
                UserName = lineProfile.UserId,
                LineId = lineProfile.UserId,
            };

            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (request.Type == UserType.Merchant)
            {
                await _merchantService.CreateMerchantByUserAndNameAsync(newUser, lineProfile.DisplayName);
            }
            else if (request.Type == UserType.Customer)
            {
                await _customerService.CreateCustomerByUserAndNameAsync(newUser, lineProfile.DisplayName);
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { Token = _jwtService.GenerateJwtToken(newUser) });
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            _logger.LogError(exception, "Login failed for user: {UserId}", user?.Id);
            return BadRequest("Registration or login failed. Please try again.");
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    //TODO: Refresh Token
    //TODO: Email Verification
    //TODO: Password Reset
}
