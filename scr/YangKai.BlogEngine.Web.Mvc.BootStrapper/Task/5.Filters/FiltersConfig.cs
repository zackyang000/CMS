using Bootstrap.Extensions.StartupTasks;
using System.Web.Mvc;

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