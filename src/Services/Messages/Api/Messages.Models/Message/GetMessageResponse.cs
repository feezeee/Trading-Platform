using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Messages.Models.Message
{
    public class GetMessageResponse
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }

        [JsonPropertyName("user_id")]
        [BindRequired]
        [Required]
        public Guid UserId { get; set; }

        [JsonPropertyName("message")]
        [BindRequired]
        [Required]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("created_date")]
        [BindRequired]
        [Required]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("chat_id")]
        [BindRequired]
        [Required]
        public Guid ChatId { get; set; }
    }
}
