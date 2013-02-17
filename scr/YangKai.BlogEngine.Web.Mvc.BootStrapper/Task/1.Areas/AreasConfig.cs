using System.Web.Mvc;
using Bootstrap.Extensions.StartupTasks;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class AreasConfig : IStartupTask
    {
        public void Run()
        {
            AreaRegistration.RegisterAllAreas();
        }

        public void Reset()
        {
        }
    }
}