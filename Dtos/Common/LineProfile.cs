using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AppointmentSystem.Dtos.Common;

public class LineProfile
{
    [JsonPropertyName("userId")]
    public required string UserId { get; set; }
    [JsonPropertyName("displayName")]
    public required string DisplayName { get; set; }
    [JsonPropertyName("pictureUrl")]
    public required string PictureUrl { get; set; }
}
