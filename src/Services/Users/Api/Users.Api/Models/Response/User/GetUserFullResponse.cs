using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Users.Api.Models.Response.Role;

namespace Users.Api.Models.Response.User
{
    public class GetUserFullResponse
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }
        [JsonPropertyName("first_name")]
        [BindRequired]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        [BindRequired]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("nickname")]
        [BindRequired]
        [Required]
        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("profile_image_url")]
        public string? ProfileImageUrl { get; set; }

        [JsonPropertyName("registration_date")]
        [BindRequired]
        [Required]
        public DateTime RegistrationDate { get; set; }

        [JsonPropertyName("roles")]
        [BindRequired]
        [Required]
        public ICollection<GetRoleResponse> Roles { get; set; } = new List<GetRoleResponse>();
    }
}
