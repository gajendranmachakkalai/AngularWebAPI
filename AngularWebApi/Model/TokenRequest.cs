namespace AngularWebApi.Model
{
    /// <summary>
    /// Request token using Refresh Token
    /// </summary>
    public class TokenRequest
    {
        public int UserId { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
