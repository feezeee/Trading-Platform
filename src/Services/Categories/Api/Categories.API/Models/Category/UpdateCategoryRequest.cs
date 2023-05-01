using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Categories.API.Models.Category
{
    public class UpdateCategoryRequest
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
