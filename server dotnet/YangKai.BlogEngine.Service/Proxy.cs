using AtomLab.Core;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Infrastructure;

namespace YangKai.BlogEngine.Service
{
    public static class Proxy
    {
        public static Repository<T> Repository<T>() where T : Entity
        {
            return InstanceLocator.Current.GetInstance<Repository<T>>();
        }

        public static UserSecurity Security()
        {
            return Config.UseNeweggAccount ? (UserSecurity)new NeweggUserSecurity() : new LocalUserSecurity();
        }
    }
}