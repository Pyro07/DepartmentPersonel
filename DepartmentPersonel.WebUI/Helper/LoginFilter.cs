using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DepartmentPersonel.WebUI.Helper
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);
            var session = context.HttpContext.Session["KullaniciEmail"];
            if(session == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Account" }, { "action", "LogOn" } }
                    );
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}