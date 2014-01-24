namespace OAuth2.Common
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Id { get; set; }
        
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Id);
            }
        }
    }
}