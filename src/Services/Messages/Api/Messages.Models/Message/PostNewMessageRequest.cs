using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Messages.Models.Message
{
    public class PostNewMessageRequest
    {
        [JsonPropertyName("from_user_id")]
        [BindRequired]
        [Required]
        public Guid FromUserId { get; set; }

        [JsonPropertyName("to_user_id")]
        [BindRequired]
        [Required]
        public Guid ToUserId { get; set; }

        [JsonPropertyName("message")]
        [BindRequired]
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
