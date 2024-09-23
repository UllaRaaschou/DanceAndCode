namespace SimoneAPI.DataModels;

using Microsoft.AspNetCore.Routing;
using System;

public class DateOnlyRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out var value) && value is string dateString)
        {
            return DateOnly.TryParse(dateString, out _);
        }

        return false;
    }
}