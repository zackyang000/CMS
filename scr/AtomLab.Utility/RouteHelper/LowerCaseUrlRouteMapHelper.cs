using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AtomLab.Utility.RouteHelper;

namespace AtomLab.Utility.RouteHelper
{
    public static class LowerCaseUrlRouteMapHelper
    {
        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            LowerCaseUrlRoute route2 = new LowerCaseUrlRoute(url, new MvcRouteHandler());
            route2.Defaults = new RouteValueDictionary(defaults);
            route2.Constraints = new RouteValueDictionary(constraints);
            route2.DataTokens = new RouteValueDictionary();
            LowerCaseUrlRoute item = route2;
            if ((namespaces != null) && (namespaces.Length > 0))
            {
                item.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, item);
            return item;
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url)
        {
            return context.MapLowerCaseUrlRoute(name, url, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static LowerCaseUrlRoute MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if ((namespaces == null) && (context.Namespaces != null))
            {
                namespaces = context.Namespaces.ToArray<string>();
            }
            LowerCaseUrlRoute route = context.Routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = context.AreaName;
            bool flag = (namespaces == null) || (namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = flag;
            return route;
        }
    }
}