using System.Web;
using AtomLab.Core;
using AtomLab.Utility;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;

[assembly: PreApplicationStartMethod(typeof(Initializer), "Initialize")]
namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public static class Initializer
    {
        public static void Initialize()
        {

            Auth.GetName = () =>
            {
                var user = Proxy.Security().AutoLogin();
                return user == null ? "unknown" : user.UserName;
            };
                
            Auth.GetIp = () => HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "unknown";

            IoCConfig.Run();
            DataInitializeConfig.Run();
            WebApiConfig.Run();
            FiltersConfig.Run();
        }
    }
}