using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Products.Api.Models.Products.Request
{
    public class PutProductRequest
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
