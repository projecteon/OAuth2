namespace OAuth2.Common
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    
    using Newtonsoft.Json;

    public abstract class OAuth2ServiceBase
    {
        private readonly IOAuth2Initializer oAuth2Initializer;
        protected abstract string RedirectUrl { get; }
        protected abstract string AuthorizationCodeStartUrl { get; }
        public abstract Uri AuthorizationCodeEndUrl { get; }
        protected abstract string AuthorizationTokenUrl { get; }
        protected abstract string UserDataUrl { get; }
        protected abstract string Scope { get; }
        protected abstract string Optional { get; }
        protected string Code { get; set; }

        protected OAuth2ServiceBase(IOAuth2Initializer oAuth2Initializer, OAuth2Client oAuth2Client)
        {
            this.oAuth2Initializer = oAuth2Initializer;
            this.ClientId = oAuth2Client.Id;
            this.ClientSecret = oAuth2Client.Secret;
        }

        private string ClientId { get; set; }

        private string ClientSecret { get; set; }

        public Uri AuthorizationCodeStartUri
        {
            get
            {
                var uri = this.AuthorizationCodeStartUrl + "?client_id=" + Uri.EscapeDataString(this.ClientId) + "&redirect_uri="
                          + Uri.EscapeDataString(this.RedirectUrl) + "&response_type=code" + "&scope=" + this.Scope + this.Optional;
                return new Uri(uri);
            }
        }

        protected abstract Task<string> GeAccessTokenFrom(string code);

        public abstract Task<string> RefreshToken();

        public async Task<UserInfo> Login()
        {
            this.Code = await this.oAuth2Initializer.RequestToken(this);
            if (string.IsNullOrEmpty(this.Code))
            {
                return new UserInfo();
            }
            var authorizationToken = await this.GeAccessTokenFrom(this.Code);
            return await this.FetchUserData(authorizationToken);
        }

        private async Task<UserInfo> FetchUserData(string token)
        {
            using (var client = new HttpClient())
            {
                var data = await client.GetAsync(this.UserDataUrl + "access_token=" + token);
                return
                    await data.Content.ReadAsStringAsync().ContinueWith(content => JsonConvert.DeserializeObject<UserInfo>(content.Result.ToString()));
            }
        }

        protected async Task<T> GeAccessToken<T>()
        {
            if (string.IsNullOrEmpty(this.Code))
            {
                throw new NotSupportedException("Authorization code is null or empty.");
            }
            var urlencoding = "redirect_uri=" + Uri.EscapeDataString(this.RedirectUrl) + "&client_id=" + this.ClientId + "&client_secret="
                              + this.ClientSecret + "&code=" + this.Code + "&grant_type=authorization_code";
            T token;
            using (var client = new HttpClient())
            {
                var data =
                    await
                        client.PostAsync(
                            this.AuthorizationTokenUrl,
                            new StringContent(urlencoding, Encoding.UTF8, "application/x-www-form-urlencoded"));
                token = await this.ParseTokenResponseFrom<T>(data);
            }
            return token;
        }

        protected abstract Task<T> ParseTokenResponseFrom<T>(HttpResponseMessage data);
    }
}