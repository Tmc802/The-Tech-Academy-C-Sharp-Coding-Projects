using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FileDataExtractor.Models
{
    public class RequireRouteValuesAttribute : ActionMethodSelectorAttribute
    {
        public string[] ValueNames { get; private set; }

        public RequireRouteValuesAttribute(string[] valueNames)
        {
            ValueNames = valueNames;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            bool contains = false;
            foreach (var value in ValueNames)
            {
                contains = controllerContext.RequestContext.RouteData.Values.ContainsKey(value);
                if (!contains) break;
            }
            return contains;
        }
    }
}

