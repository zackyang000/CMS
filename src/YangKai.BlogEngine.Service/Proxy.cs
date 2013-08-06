using AtomLab.Core;
using YangKai.BlogEngine.Infrastructure;

namespace YangKai.BlogEngine.Service
{
    public static class Proxy
    {
        public static Repository<T> Repository<T>() where T : Entity
        {
            return InstanceLocator.Current.GetInstance<Repository<T>>();
        }
    }
}