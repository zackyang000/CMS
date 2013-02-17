using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AtomLab.Domain;
using Bootstrap.Extensions.StartupTasks;
using YangKai.BlogEngine.Infrastructure;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class DomainConfig : IStartupTask
    {
        public void Run()
        {
            var assemblys = new List<Assembly>
                {
                    typeof (Board).Assembly,
                    typeof (Post).Assembly,
                    typeof (User).Assembly,
                };

            //将所有Event和EventHandler建立映射关系
            assemblys.ForEach(EventHandlerMappingStore.Current.RegisterAllHandlerTypesFromAssembly);
        }

        public void Reset()
        {

        }
    }
}