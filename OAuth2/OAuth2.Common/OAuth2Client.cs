namespace OAuth2.Common
{
    public class OAuth2Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Id) && !string.IsNullOrEmpty(this.Secret);
        }
    }
}