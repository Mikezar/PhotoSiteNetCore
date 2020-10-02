﻿using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin.Authorize
{
    public class LoginStateDto
    {
        [JsonPropertyName("status")]
        public LoginStatusDto Status { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}