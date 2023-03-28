﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Users.Api.Models.Request.Registration
{
    public class RegistrateUserRequest
    {
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
