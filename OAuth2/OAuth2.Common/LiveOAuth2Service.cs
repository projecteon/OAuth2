namespace OAuth2.Common
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class LiveOAuth2Service : OAuth2ServiceBase
    {
        public LiveOAuth2Service(IOAuth2Initializer oAuth2Initializer, OAuth2Client oAuth2Client)
            : base(oAuth2Initializer, oAuth2Client)
        {
        }

        protected override string RedirectUrl
        {
            get
            {
                return "https://login.live.com/oauth20_desktop.srf";
            }
        }

        protected override string AuthorizationCodeStartUrl
        {
            get
            {
                return @"https://login.live.com/oauth20_authorize.srf";
            }
        }

        public override Uri AuthorizationCodeEndUrl
        {
            get
            {
                return new Uri(this.RedirectUrl);
            }
        }

        protected override string AuthorizationTokenUrl
        {
            get
            {
                return @"https://login.live.com/oauth20_token.srf";
            }
        }

        protected override string UserDataUrl
        {
            get
            {
                return @"https://apis.live.net/v5.0/me?";
            }
        }

        protected override string Scope
        {
            get
            {
                return "wl.offline_access,wl.emails,wl.basic";
            }
        }

        protected override string Optional
        {
            get
            {
                return "&display=touch";
            }
        }

        protected override async Task<string> GeAccessTokenFrom(string code)
        {
            this.Code = code;
            return (await base.GeAccessToken<LiveToken>()).access_token;
        }

        public override Task<string> RefreshToken()
        {
            throw new System.NotImplementedException();
        }

        private class LiveToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string scope { get; set; }
            public string refresh_token { get; set; }
        }

        protected override async Task<T> ParseTokenResponseFrom<T>(HttpResponseMessage data)
        {
            return await
                data.Content.ReadAsStringAsync()
                    .ContinueWith(content => JsonConvert.DeserializeObject<T>(content.Result.ToString()));
        }
    }
}