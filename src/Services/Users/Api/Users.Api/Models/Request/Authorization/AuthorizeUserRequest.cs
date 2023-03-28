using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.Authorization
{
    public class AuthorizeUserRequest
    {
        [JsonPropertyName("email")]
        [BindRequired]
        [Required]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        [BindRequired]
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
