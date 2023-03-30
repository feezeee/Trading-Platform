using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.Role
{
    public class CreateRoleRequest
    {
        [JsonPropertyName("name")]
        [BindRequired]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
