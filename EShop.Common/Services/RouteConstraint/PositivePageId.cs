using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EShop.Common.Services.RouteConstraint
{
    public class PositivePageId : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (values[routeKey] != null)
            {
                return long.Parse(values[routeKey].ToString()) > 0;
            }

            return true;
        }
    }
}
