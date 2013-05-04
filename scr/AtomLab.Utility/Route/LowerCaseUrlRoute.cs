using System;
using System.Linq;
using System.Globalization;
using System.Web.Routing;

namespace AtomLab.Utility.RouteHelper
{
    public class LowerCaseUrlRoute : Route
    {
        private static readonly string[] requiredKeys = new [] { "area", "controller", "action" };

        public LowerCaseUrlRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        { }
        
        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        { }
        
        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }
        

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            LowerRouteValues(requestContext.RouteData.Values);
            LowerRouteValues(values);
            LowerRouteValues(Defaults);

            return base.GetVirtualPath(requestContext, values);
        }


        private void LowerRouteValues(RouteValueDictionary values)
        {
            foreach (var key in requiredKeys)
            {
                if (values.ContainsKey(key) == false) continue;

                var value = values[key];
                if (value == null) continue;

                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);

                values[key] = valueString.ToLower();
            }

            var otherKyes = values.Keys.ToArray()
                .Except(requiredKeys, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();

            foreach (var key in otherKyes)
            {
                var value = values[key];
                values.Remove(key);
                values.Add(key.ToLower(), value);
            }
        }
    }
}
