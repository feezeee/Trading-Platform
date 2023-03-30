using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Users.Api.Models.Request.RoleSetUp
{
    public class RoleSetUpRequest
    {
        [JsonPropertyName("user_id")]
        [BindRequired]
        [Required]
        public Guid UserId { get; set; }

        [JsonPropertyName("role_ids")]
        [BindRequired]
        [Required]
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}
