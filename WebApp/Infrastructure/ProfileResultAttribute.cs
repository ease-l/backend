using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Controllers;
using WebApp.Areas.HelpPage.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class ProfileResultAttribute : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Http.Filters.IExceptionFilter
    {

        /*public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            String add = "{{\"value\":";
            if (!filterContext.Controller.GetType().Equals(typeof(HomeController)) &&
                !filterContext.Controller.GetType().Equals(typeof(MongoDBController)) &&
                !filterContext.Controller.GetType().Equals(typeof(ManageController)) &&
                !filterContext.Controller.GetType().Equals(typeof(HelpController)))
            {
                filterContext.HttpContext.Response.Write(add);
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            String add = "}}";
            if (!filterContext.Controller.GetType().Equals(typeof(HomeController)) &&
                !filterContext.Controller.GetType().Equals(typeof(MongoDBController)) &&
                !filterContext.Controller.GetType().Equals(typeof(ManageController)) &&
                !filterContext.Controller.GetType().Equals(typeof(HelpController)))
            {
                filterContext.HttpContext.Response.Write(add);
            }
        }*/
        public void OnException(ExceptionContext filterContext)
        {
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                // ... тут делаем грязь с actionExecutedContext.Response.Content
            }
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var exception = actionExecutedContext.Exception;
            var response = new { Error = exception };
            var message = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            message.Content = new ObjectContent(response.GetType(), response, actionExecutedContext.Request.Content.Headers.ContentType.MediaType.);
        }
    }
}