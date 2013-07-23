using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web.Security;
using Bootstrap.Extensions.StartupTasks;
using YangKai.BlogEngine.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
  public static class Initializer
    {
      public static void Initialize()
      {
          Bootstrap.Bootstrapper.With.StartupTasks()
                   .UsingThisExecutionOrder(s => s
                                                     .First<DataInitializeConfig>()
                                                     .DelayStartBy(1).MilliSeconds
                                                     .Then<AreasConfig>()
                                                     .DelayStartBy(1).MilliSeconds
                                                     .Then<IoCConfig>()
                                                     .DelayStartBy(1).MilliSeconds
                                                     .Then<DomainConfig>()
                                                     .DelayStartBy(1).MilliSeconds
                                                     .Then<WebApiConfig>()
                                                     .Then<RoutesConfig>()
                                                     .Then<FiltersConfig>()
                                                     .Then<BundlesConfig>()
              ).Start();
      }
    }
}
