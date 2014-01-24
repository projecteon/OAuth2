namespace OAuth2.WindowsStore
{
    using Windows.Storage;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using OAuth2.Common;

    public class AuthorizedUser : IAuthorizedUser
    {
        private static AuthorizedUser instace;

        public static AuthorizedUser Instance
        {
            get
            {
                if (instace == null)
                {
                    instace = new AuthorizedUser();
                }
                return instace;
            }
        }

        public AuthorizedUser()
        {
            var userInfoJson = this.LoadSafelyFromLocalSettings("OAuth2UserInfo");
            var oAuth2ServiceJson = this.LoadSafelyFromLocalSettings("OAuth2Service");
            if (string.IsNullOrEmpty(userInfoJson) || string.IsNullOrEmpty(oAuth2ServiceJson))
            {
                return;
            }

            this.UserInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoJson);
            this.OAuth2Service = JsonConvert.DeserializeObject<FacebookOAuth2Service>(oAuth2ServiceJson);
        }

        private string LoadSafelyFromLocalSettings(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key].ToString();
            }
            return null;
        }

        public OAuth2ServiceBase OAuth2Service { get; private set; }

        public bool IsAuthenticated
        {
            get
            {
                return OAuth2Service != null && UserInfo != null;
            }
        }

        public UserInfo UserInfo { get; private set; }
        
        private static void StoreSetting(string key, object value)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;
        }

        public void Store(UserInfo user, OAuth2ServiceBase service)
        {
            this.UserInfo = user;
            this.OAuth2Service = service;
            StoreSetting("OAuth2UserInfo", JsonConvert.SerializeObject(UserInfo, new JsonSerializerSettings(){ContractResolver = new DefaultContractResolver(){SerializeCompilerGeneratedMembers = true}}));
            StoreSetting("OAuth2Service", JsonConvert.SerializeObject(OAuth2Service));
        }
    }
}