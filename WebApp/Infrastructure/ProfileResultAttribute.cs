using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Controllers;
using WebApp.Areas.HelpPage.Controllers;
using System.Web.Http.Filters;

namespace WebApp.Infrastructure
{
    public class ProfileResultAttribute : System.Web.Http.Filters.ActionFilterAttribute, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            String add = "{\"value\":";
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
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "PUT, DELETE, Post ");
            filterContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers"," X-PINGOTHER, Content-Type");
            String add = "}";
            if (!filterContext.Controller.GetType().Equals(typeof(HomeController)) &&
                !filterContext.Controller.GetType().Equals(typeof(MongoDBController)) &&
                !filterContext.Controller.GetType().Equals(typeof(ManageController)) &&
                !filterContext.Controller.GetType().Equals(typeof(HelpController)))
            {
                filterContext.HttpContext.Response.Write(add);
            }
        }
    }
}