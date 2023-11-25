namespace AngularWebApi.Model
{
    public class JwtResponseModel
    {
        public int ClientId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }
        public List<string> Claims { get; set; }
    }
}
