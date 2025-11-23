namespace CinemaDbLibrary.DTOs
{
    public class TokenResponse(string jwtToken, string? refreshToken = null)
    {
        public string Token { get; set; } = jwtToken;
        public string? RefreshToken { get; set; } = refreshToken;
    }
}
