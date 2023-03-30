﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.User
{
    public class UpdateUserRequest
    {
        [JsonPropertyName("id")]
        [BindRequired]
        [Required]
        public Guid Id { get; set; }
        [JsonPropertyName("first_name")]
        [BindRequired]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        [BindRequired]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("nickname")]
        [BindRequired]
        [Required]
        public string Nickname { get; set; } = string.Empty;
    }
}
