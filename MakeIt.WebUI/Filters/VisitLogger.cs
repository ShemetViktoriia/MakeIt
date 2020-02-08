using MakeIt.EF;
using System;
using System.Web.Mvc;
using System.Linq;

namespace MakeIt.WebUI.Filters
{
    public class VisitLoggerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            Visitor visitor = new Visitor()
            {
                Login = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "null",
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Url = request.RawUrl,
                Date = DateTime.UtcNow
            };

            using (var db = new MakeItContext())
            {
                db.Visitors.Add(visitor);
                db.SaveChanges();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}