using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Summary", action = "Index", id = UrlParameter.Optional },
                 new[] { "YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers" }
            );
        }
    }
}
