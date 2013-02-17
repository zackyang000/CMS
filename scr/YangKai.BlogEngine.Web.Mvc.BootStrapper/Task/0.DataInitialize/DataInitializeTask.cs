using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Bootstrap.Extensions.StartupTasks;
using YangKai.BlogEngine.Infrastructure;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class DataInitializeTask : IStartupTask
    {
        public void Run()
        {
            using (var context = new BlogEngineContext())
            {
                if (!context.Database.Exists())
                {
                    //初始化数据
                    var stream = System.IO.File.OpenText(System.Web.HttpContext.Current.Server.MapPath("/bin/Task/0.DataInitialize/data.sql"));
                    var buffer = new char[stream.BaseStream.Length];
                    stream.Read(buffer, 0, Convert.ToInt32(stream.BaseStream.Length));
                    var sql = string.Concat(buffer);
                    var objectContext = ((IObjectContextAdapter)(context)).ObjectContext;
                    objectContext.ExecuteStoreCommand(sql);
                }
            }
        }

        public void Reset()
        {
        }
    }
}
