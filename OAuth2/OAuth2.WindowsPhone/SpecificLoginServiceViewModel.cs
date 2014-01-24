namespace OAuth2.WindowsPhone
{
    using Windows.Storage;

    using OAuth2.Common;

    public abstract class SpecificLoginServiceViewModel
    {
        private readonly OAuth2ServiceBase oAuth2Service;
        protected OAuth2Client oAuth2Client;

        public SpecificLoginServiceViewModel()
        {
            this.LoginCommand = new RelayCommand(this.Login);
            this.oAuth2Service = this.GetOAuth2Service();
        }

        protected OAuth2Client LoadSafelyFromLocalSettings(OAuth2ServiceConstants serviceConstant)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var oauthClient = new OAuth2Client();
            if (localSettings.Values.ContainsKey(serviceConstant.ClientId))
            {
                oauthClient.Id = localSettings.Values[serviceConstant.ClientId].ToString();
            }

            if (localSettings.Values.ContainsKey(serviceConstant.ClientSecret))
            {
                oauthClient.Secret = localSettings.Values[serviceConstant.ClientSecret].ToString();
            }

            return oauthClient;
        }

        public RelayCommand LoginCommand
        {
            get;
            private set;
        }

        private async void Login()
        {
            this.User = await oAuth2Service.Login();
        }

        protected abstract OAuth2ServiceBase GetOAuth2Service();

        public UserInfo User { get; set; }
    }
}
