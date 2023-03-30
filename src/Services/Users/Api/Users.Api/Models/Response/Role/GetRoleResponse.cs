using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Response.Role
{
    public class GetRoleResponse
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [BindRequired]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
