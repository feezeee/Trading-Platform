using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Products.Api.Models.Products.Request
{
    public class PostProductRequest
    {
        [JsonPropertyName("name")]
        [BindRequired]
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        [BindRequired]
        [Required]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("phone_numbers")]
        [BindRequired]
        [Required]
        public List<string> PhoneNumbers { get; set; } = new List<string>();

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
    }
}
