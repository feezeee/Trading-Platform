﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Users.Api.Models.Request.Authorization
{
    public class AuthorizeUserRequest
    {
        [JsonPropertyName("nickname")]
        [BindRequired]
        [Required]
        public string Nickname { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        [BindRequired]
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
