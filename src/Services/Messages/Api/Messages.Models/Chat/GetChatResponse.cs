using Messages.Models.Message;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Messages.Models.Chat
{
    public class GetChatResponse
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }

        [JsonPropertyName("user_id_arr")]
        [BindRequired]
        [Required]
        public List<Guid> Users { get; set; } = new List<Guid>();

        [JsonPropertyName("message_arr")]
        [BindRequired]
        [Required]
        public List<GetMessageResponse> Messages { get; set; } = new List<GetMessageResponse>();
    }
}
