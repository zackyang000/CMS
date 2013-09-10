using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.Data.Edm;
using Newtonsoft.Json;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class WebApiConfig : IStartupTask
    {
        public void Run()
        {
            RegisterRoutes(GlobalConfiguration.Configuration);
        }

        public void Reset()
        {
        }

        private void RegisterRoutes(HttpConfiguration config)
        {
            config.EnableQuerySupport();

            var modelBuilder = new ODataConventionModelBuilder();
            SetEntity(modelBuilder);
            SetAction(modelBuilder);
            var model = modelBuilder.GetEdmModel();

            config.Routes.MapODataRoute(
                routeName: "OData",
                routePrefix: "odata",
                model: model);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }

        private void SetAction(ODataConventionModelBuilder modelBuilder)
        {
            //管理员登陆
            var signin = modelBuilder.Entity<User>().Action("Signin");
            signin.Parameter<string>("Username");
            signin.Parameter<string>("Password");
            signin.Parameter<bool>("IsRemember");

            //管理员注销
            var signout = modelBuilder.Entity<User>().Action("Signout");

            //增加文章浏览数
            var browsed = modelBuilder.Entity<Post>().Action("Browsed");

            //增加文章评论数
            var commented = modelBuilder.Entity<Post>().Action("Commented");

            modelBuilder.Entity<Post>().Action("Remove");
            modelBuilder.Entity<Post>().Action("Recover");
            modelBuilder.Entity<Comment>().Action("Remove");
            modelBuilder.Entity<Comment>().Action("Recover");
            modelBuilder.Entity<Board>().Action("Remove");
            modelBuilder.Entity<Board>().Action("Recover");
        }

        private void SetEntity(ODataConventionModelBuilder modelBuilder)
        {
            var user = modelBuilder.EntitySet<User>("User");
            user.EntityType.Ignore(p => p.Password);
            modelBuilder.EntitySet<Log>("Log");
            modelBuilder.EntitySet<Channel>("Channel");
            modelBuilder.EntitySet<Group>("Group");
            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Comment>("Comment");
            modelBuilder.EntitySet<Board>("Board");
            modelBuilder.EntitySet<Post>("Article");
            modelBuilder.EntitySet<Tag>("Tag");
            modelBuilder.EntitySet<Thumbnail>("Thumbnail");
            modelBuilder.EntitySet<Source>("Source");
            modelBuilder.EntitySet<QrCode>("QrCode");
        }
    }
}