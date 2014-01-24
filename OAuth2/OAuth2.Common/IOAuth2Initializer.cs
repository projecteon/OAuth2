namespace OAuth2.Common
{
    using System.Threading.Tasks;

    public interface IOAuth2Initializer
    {
        Task<string> RequestToken(OAuth2ServiceBase service);
    }
}
