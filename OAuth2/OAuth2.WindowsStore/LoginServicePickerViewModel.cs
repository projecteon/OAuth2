namespace OAuth2.WindowsStore
{
    using System.Threading.Tasks;

    using Windows.Storage;

    using OAuth2.Common;

    public class LoginServicePickerViewModel
    {
        private readonly OAuth2Client googleClient;
        private readonly OAuth2Client facebookClient;
        private readonly OAuth2Client liveClient;

        public LoginServicePickerViewModel()
        {

            this.googleClient = this.LoadSafelyFromLocalSettings(OAuth2ServiceConstants.GoogleOAuth2Service);
            this.facebookClient = this.LoadSafelyFromLocalSettings(OAuth2ServiceConstants.FacebookOAuth2Service);
            this.liveClient = this.LoadSafelyFromLocalSettings(OAuth2ServiceConstants.LiveOAuth2Service);

            this.GoogleLoginCommand = new RelayCommand(this.GoogleLogin);
            this.FacebookLoginCommand = new RelayCommand(this.FacebookLogin);
            this.LiveLoginCommand = new RelayCommand(this.LiveLogin);
        }

        private OAuth2Client LoadSafelyFromLocalSettings(OAuth2ServiceConstants serviceConstant)
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

        public RelayCommand GoogleLoginCommand { get; private set; }

        private async void GoogleLogin()
        {
            var service = new GoogleOAuth2Service(new OAuth2Initializer(), googleClient);
            await this.HandleLogin(service);
        }

        public RelayCommand FacebookLoginCommand { get; private set; }

        private async void FacebookLogin()
        {
            var service = new FacebookOAuth2Service(new OAuth2Initializer(), facebookClient);
            await this.HandleLogin(service);
        }

        public RelayCommand LiveLoginCommand { get; private set; }

        private async void LiveLogin()
        {
            var service = new LiveOAuth2Service(new OAuth2Initializer(), liveClient);
            await this.HandleLogin(service);
        }

        private async Task HandleLogin(OAuth2ServiceBase service)
        {
            this.User = await service.Login();
            if (this.User.IsValid)
            {
                AuthorizedUser.Instance.Store(this.User, service);
            }
        }

        private UserInfo User { get; set; }

        public bool CanGoogleLogin
        {
            get
            {
                return googleClient.IsValid();
            }
        }

        public bool CanFacebookLogin
        {
            get
            {
                return facebookClient.IsValid();
            }
        }

        public bool CanLiveLogin
        {
            get
            {
                return liveClient.IsValid();
            }
        }
    }
}