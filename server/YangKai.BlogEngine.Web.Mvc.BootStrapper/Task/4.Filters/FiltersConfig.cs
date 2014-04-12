using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bootstrap.Extensions.StartupTasks;
using System.Web.Http;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class FiltersConfig : IStartupTask
    {
        public void Run()
        {
            GlobalConfiguration.Configuration.Filters.Add(new ProfilerFilter());
        }

        public void Reset()
        {
        }
    }
}