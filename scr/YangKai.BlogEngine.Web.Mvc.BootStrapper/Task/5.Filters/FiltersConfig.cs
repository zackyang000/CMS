using System.Linq;
using System.Web.Routing;
using AtomLab.Utility.RouteHelper;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class FiltersConfig : IStartupTask
    {
        public void Run()
        {
           GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        public void Reset()
        {
        }
    }
}