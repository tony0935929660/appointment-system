using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AppointmentSystem.Models;
using AppointmentSystem.Dtos.Common;

namespace AppointmentSystem.Services;

public class LineService
{
    private readonly IConfiguration _configuration;

    public LineService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<LineProfile> GetLineProfileAsync(string accessToken)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.GetAsync("https://api.line.me/v2/profile");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to retrieve LINE profile.");
        }

        var profileJson = await response.Content.ReadAsStringAsync();
        var profile = System.Text.Json.JsonSerializer.Deserialize<LineProfile>(profileJson);

        return profile!;
    }
}
