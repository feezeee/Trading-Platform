﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Products.Api.Models.Products.Response
{
    public class GetProductResponse
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [BindRequired]
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        [BindRequired]
        [Required]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("image_urls")]
        [BindRequired]
        [Required]
        public List<string> ImageUrls { get; set; } = new List<string>();

        [JsonPropertyName("phone_numbers")]
        [BindRequired]
        [Required]
        public List<string> PhoneNumbers { get; set; } = new List<string>();

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("category_id_list")]
        [BindRequired]
        [Required]
        public List<Guid> CategoryIdList { get; set; } = new List<Guid>();

        [JsonPropertyName("created_at")]
        [BindRequired]
        [Required]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("user_id")]
        [BindRequired]
        [Required]
        public Guid UserId { get; set; }
    }
}
