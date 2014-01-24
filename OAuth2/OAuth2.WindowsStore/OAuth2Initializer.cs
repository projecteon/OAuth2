namespace OAuth2.WindowsStore
{
    using System;
    using System.Threading.Tasks;

    using Windows.Security.Authentication.Web;

    using OAuth2.Common;

    public class OAuth2Initializer : IOAuth2Initializer
    {
        public async Task<string> RequestToken(OAuth2ServiceBase service)
        {
            try
            {
                WebAuthenticationResult webAuthenticationResult =
                    await
                        WebAuthenticationBroker.AuthenticateAsync(this.GetWebAuthenticationOptions(service), service.AuthorizationCodeStartUri, service.AuthorizationCodeEndUrl);
                if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    return this.ParseAuthorizationCodeFrom(webAuthenticationResult.ResponseData);
                }
                else if (webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    // do something when the request failed
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            catch (Exception)
            {
                // do something when an exception occurred
            }
            return null;
        }

        

        private WebAuthenticationOptions GetWebAuthenticationOptions(OAuth2ServiceBase service)
        {
            if (service.GetType() == typeof(GoogleOAuth2Service))
            {
                return WebAuthenticationOptions.UseTitle;
            }
            return WebAuthenticationOptions.None;
        }

        private string ParseAuthorizationCodeFrom(string responseString)
        {
            var indexOfToken = (responseString.IndexOf("code=", StringComparison.Ordinal) + 5);
            return (responseString.Substring(indexOfToken, responseString.Length - indexOfToken));
        }
    }
}
