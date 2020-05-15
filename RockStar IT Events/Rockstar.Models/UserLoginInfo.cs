namespace Rockstar.Models
{
    public class UserLoginInfo
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string role { get; set; }
    }
}