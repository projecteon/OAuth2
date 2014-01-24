namespace OAuth2.Common
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class FacebookOAuth2Service : OAuth2ServiceBase
    {
        public FacebookOAuth2Service(IOAuth2Initializer oAuth2Initializer, OAuth2Client oAuth2Client)
            : base(oAuth2Initializer, oAuth2Client)
        {
        }
        
        protected override string RedirectUrl
        {
            get
            {
                return @"https://www.facebook.com/connect/login_success.html";
            }
        }

        protected override string AuthorizationCodeStartUrl
        {
            get
            {
                return @"https://graph.facebook.com/oauth/authorize";
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
                return @"https://graph.facebook.com/oauth/access_token";
            }
        }

        protected override string UserDataUrl
        {
            get
            {
                return @"https://graph.facebook.com/me?fields=email&format=json&";
            }
        }

        protected override string Scope
        {
            get
            {
                return "email";
            }
        }

        protected override string Optional
        {
            get
            {
                return "&display=touch&response_type=code";
            }
        }

        protected override async Task<string> GeAccessTokenFrom(string code)
        {
            this.Code = code;
            return (await base.GeAccessToken<FaceBook>()).access_token;
        }

        public override Task<string> RefreshToken()
        {
            throw new System.NotImplementedException();
        }

        protected override async Task<T> ParseTokenResponseFrom<T>(HttpResponseMessage data)
        {
            var queryStringResult = data.Content.ReadAsStringAsync().Result;
            var json = queryStringResult.ParseQueryString();
            return await Task.FromResult<T>(JsonConvert.DeserializeObject<T>(json));
        }

        private class FaceBook
        {
            public string access_token { get; set; }
            public string expires { get; set; }
        }
    }
}
