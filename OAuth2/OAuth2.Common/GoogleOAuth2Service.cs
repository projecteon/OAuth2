namespace OAuth2.Common
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class GoogleOAuth2Service : OAuth2ServiceBase
    {
        public GoogleOAuth2Service(IOAuth2Initializer oAuth2Initializer, OAuth2Client oAuth2Client)
            : base(oAuth2Initializer, oAuth2Client)
        {
        }

        protected override string RedirectUrl
        {
            get
            {
                return "urn:ietf:wg:oauth:2.0:oob";
            }
        }

        protected override string AuthorizationCodeStartUrl
        {
            get
            {
                return @"https://accounts.google.com/o/oauth2/auth";
            }
        }

        public override Uri AuthorizationCodeEndUrl
        {
            get
            {
                return new Uri(@"https://accounts.google.com/o/oauth2/approval?");
            }
        }

        protected override string AuthorizationTokenUrl
        {
            get
            {
                return @"https://accounts.google.com/o/oauth2/token";
            }
        }

        protected override string UserDataUrl
        {
            get
            {
                return @"https://www.googleapis.com/oauth2/v1/userinfo?";
            }
        }

        protected override string Scope
        {
            get
            {
                return "email," + Uri.EscapeDataString("https://www.googleapis.com/auth/userinfo.profile");
            }
        }

        protected override string Optional
        {
            get
            {
                return "&access_type=offline";
            }
        }

        protected override async Task<string> GeAccessTokenFrom(string code)
        {
            this.Code = code;
            return (await base.GeAccessToken<GoogleToken>()).access_token;
        }

        public override Task<string> RefreshToken()
        {
            throw new System.NotImplementedException();
        }

        protected override async Task<T> ParseTokenResponseFrom<T>(HttpResponseMessage data)
        {
            return await
                data.Content.ReadAsStringAsync()
                    .ContinueWith(content => JsonConvert.DeserializeObject<T>(content.Result.ToString()));
        }

        private class GoogleToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
    }
}
