namespace OAuth2.WindowsPhone
{
    using OAuth2.Common;

    public class LoginServicePickerViewModel : BindableBase
    {
        private bool showGoogleLogin;
        private bool showCancel;
        private bool showLoginServiceButtons;

        public LoginServicePickerViewModel()
        {
            this.GoogleLoginCommand = new RelayCommand(this.GoogleLogin);
            this.FacebookLoginCommand = new RelayCommand(this.FacebookLogin);
            this.LiveLoginCommand = new RelayCommand(this.LiveLogin);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.ShowLoginServiceButtons = true;
        }

        private void Cancel()
        {
            ShowServiceButtons();
        }

        private void ShowServiceButtons()
        {
            this.ShowLoginServiceButtons = true;
            this.ShowGoogleLogin = false;
            this.ShowCancel = false;
        }

        public RelayCommand GoogleLoginCommand
        {
            get;
            private set;
        }

        private void GoogleLogin()
        {
            this.HideServiceButtons();
        }

        private void HideServiceButtons()
        {
            this.ShowLoginServiceButtons = false;
            this.ShowGoogleLogin = true;
            this.ShowCancel = true;
        }

        public RelayCommand FacebookLoginCommand
        {
            get;
            private set;
        }

        private void FacebookLogin()
        {
            this.Login<FacebookOAuth2Service>();
        }

        public RelayCommand LiveLoginCommand
        {
            get;
            private set;
        }

        private void LiveLogin()
        {
            this.Login<LiveOAuth2Service>();
        }

        public bool ShowGoogleLogin
        {
            get
            {
                return this.showGoogleLogin;
            }
            private set
            {
                this.SetProperty(ref this.showGoogleLogin, value);
            }
        }

        public bool ShowCancel
        {
            get
            {
                return this.showCancel;
            }
            set
            {
                this.SetProperty(ref this.showCancel, value);
            }
        }

        private void Login<T>() where T : OAuth2ServiceBase
        {
            //var service = Botox.CreateInstanceOf<T>();
            //service.Login();
            //this.User = service.User;
        }

        public UserInfo User { get; set; }

        public RelayCommand CancelCommand { get; private set; }

        public bool ShowLoginServiceButtons
        {
            get
            {
                return this.showLoginServiceButtons;
            }
            set
            {
                this.SetProperty(ref this.showLoginServiceButtons, value);
            }
        }
    }
}
