namespace OAuth2.Common
{
    using Constant;

    public class OAuth2ServiceConstants : Constant<OAuth2ServiceConstants>
    {
        public static readonly OAuth2ServiceConstants GoogleOAuth2Service = new OAuth2ServiceConstants("GoogleOAuth2Service", "GoogleClientId", "GoogleClientSecret");
        public static readonly OAuth2ServiceConstants FacebookOAuth2Service = new OAuth2ServiceConstants("FacebookOAuth2Service", "FacebookClientId", "FacebookClientSecret");
        public static readonly OAuth2ServiceConstants LiveOAuth2Service = new OAuth2ServiceConstants("LiveOAuth2Service", "LiveClientId", "LiveClientSecret");

        public OAuth2ServiceConstants(string key, string clientId, string clientSecret)
        {
            this.ClientSecret = clientSecret;
            this.ClientId = clientId;
            this.Add(key, this);
        }

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
    }
}
