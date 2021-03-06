namespace TestWebApp.Common.Helpers.Authentication
{
    public class JwtAuth
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double ExpirationMinutes { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}