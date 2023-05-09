using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.CurrentUser
{
    public class PostNewPasswordRequest
    {
        [JsonPropertyName("password")]
        [BindRequired]
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
