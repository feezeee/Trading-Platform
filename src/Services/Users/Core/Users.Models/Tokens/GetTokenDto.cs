namespace Users.Models.Tokens
{
    public class GetTokenDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime RefreshTokenExpired { get; set; }

    }
}
