namespace OAuth2.Common
{
    public interface IAuthorizedUser
    {
        OAuth2ServiceBase OAuth2Service { get; }
        bool IsAuthenticated { get; }
        UserInfo UserInfo { get;  }

        void Store(UserInfo user, OAuth2ServiceBase service);
    }
}
