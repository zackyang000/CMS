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

        public static IUserSecurity Security()
        {
            return Config.UseNeweggAccount ? (IUserSecurity)new NeweggUserSecurity() : new LocalUserSecurity();
        }
    }
}