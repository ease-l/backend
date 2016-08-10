using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Controllers;
using WebApp.Areas.HelpPage.Controllers;

namespace WebApp.Infrastructure
{
    public class ProfileResultAttribute : FilterAttribute, IResultFilter
    {

        public void OnResultExecuting(ResultExecutingContext filterContext)
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
        }
    }
}