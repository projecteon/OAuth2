
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace OAuth2.WindowsStore.Tests.Integration
{
    using Windows.Storage;

    using System;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using OAuth2.Common;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            //var localSettings = ApplicationData.Current.LocalSettings;
            //localSettings.Values.Remove(OAuth2ServiceConstants.GoogleOAuth2Service.ClientId);
            //localSettings.Values.Remove(OAuth2ServiceConstants.GoogleOAuth2Service.ClientSecret);
            AddClientIdAndSecretToSettings(OAuth2ServiceConstants.GoogleOAuth2Service, "687998577788-16cd6k3uc8dvl8vn9u7aubd00oq0reeu.apps.googleusercontent.com", "LAgh2YL53ZnhyMT-1QEXXq1K");
            AddClientIdAndSecretToSettings(OAuth2ServiceConstants.FacebookOAuth2Service, "178843272298136", "8da0d249c08854513666974dedd7a09d");
            AddClientIdAndSecretToSettings(OAuth2ServiceConstants.LiveOAuth2Service, "0000000044100105", "JAXcdOxYk9G4zWWmpvxTrbe-w8BxZycm");

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            
            if (AuthorizedUser.Instance.IsAuthenticated)
            {
                // Access data in roamingSettings.containers.lookup("exampleContainer").values.hasKey("exampleSetting");
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        private static void AddClientIdAndSecretToSettings(OAuth2ServiceConstants oAuth2ServiceConstant, string clientId, string clientSecret)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[oAuth2ServiceConstant.ClientId] = clientId;
            localSettings.Values[oAuth2ServiceConstant.ClientSecret] = clientSecret;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
