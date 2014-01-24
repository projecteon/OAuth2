namespace OAuth2.WindowsPhone
{
    using OAuth2.Common;

    public class GoogleLoginServiceViewModel : SpecificLoginServiceViewModel
    {
        public GoogleLoginServiceViewModel()
        {
            this.oAuth2Client = this.LoadSafelyFromLocalSettings(OAuth2ServiceConstants.GoogleOAuth2Service);
        }

        protected override OAuth2ServiceBase GetOAuth2Service()
        {
            return new GoogleOAuth2Service(new OAuth2Initializer(), this.oAuth2Client);
        }
    }
}
