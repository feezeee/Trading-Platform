using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.RefreshToken
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("access_token")]
        [BindRequired]
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        [BindRequired]
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
