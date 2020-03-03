using LFAdmin.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFAdmin.Views.Filters
{
    /// <summary>
    /// 权限验证 filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        ReturnData rd = new ReturnData();

        #region 多登陆页跳转
        private string _RedirectUrl = null;
         
        public CustomAuthorizeAttribute(string redirectUrl = "~/Home/Login")
        {
            this._RedirectUrl = redirectUrl;
        }
        #endregion
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //如果为匿名特性,则不需要控制
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true))
            {
                var JGLoginAccount = filterContext.HttpContext.Session["JGLoginAccount"];  
                //if (JGLoginAccount != null && !JGLoginAccount.Equals(""))
                //{ 
                //}
                //else
                //{
                //    //如果是ajax请求,返回ajax结果
                //    if(filterContext.HttpContext.Request.IsAjaxRequest())
                //    {
                //        rd.IsSuccess = "0";
                //        rd.MessageString = "登录超时,请返回登录!";
                //        filterContext.Result = new JsonResult()
                //        {
                //            Data = rd
                //        }; 
                        
                //    }
                //    else
                //    {
                //        filterContext.HttpContext.Response.Redirect(_RedirectUrl);//跳转到登录页 
                //        filterContext.Result = new EmptyResult(); //防止执行后续的代码  

                //        //登陆后跳转原页面 将跳转之前的url保存到session
                //        filterContext.HttpContext.Session["CurrentUrl"] = filterContext.HttpContext.Request.Url.ToString();
                //    } 
                //} 
                return;
            }

        }
    }
}