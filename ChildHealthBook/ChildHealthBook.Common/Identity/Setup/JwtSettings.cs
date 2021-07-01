namespace Common.Identity.Setup
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string SecretCode { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string Audience { get; set; }
    }
}
