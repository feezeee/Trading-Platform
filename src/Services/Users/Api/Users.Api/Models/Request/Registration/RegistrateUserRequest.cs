using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.User
{
    public class RegistrateUserRequest
    {
        [JsonPropertyName("first_name")]
        [BindRequired]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        [BindRequired]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("nick_name")]
        [BindRequired]
        [Required]
        public string NickName { get; set; } = string.Empty;
    }
}
