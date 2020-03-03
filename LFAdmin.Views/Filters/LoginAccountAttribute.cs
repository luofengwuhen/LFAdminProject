using LFAdmin.Views.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LFAdmin.Views
{

    public class LoginAccountAttribute : ActionFilterAttribute
    {
        public bool IsLogin { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var JGLoginAccount = filterContext.HttpContext.Session["JGLoginAccount"];
            if (IsLogin == false)
            {
                if (JGLoginAccount != null && !JGLoginAccount.Equals(""))
                {


                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Login?time=" + DateTime.Now);//跳转到登录页 
                    filterContext.Result = new EmptyResult(); //防止执行后续的代码 
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    //{
                    //    controller = "Home",
                    //    action = "Login"
                    //})); 
                }
            }
            return;
        }

    }
}
