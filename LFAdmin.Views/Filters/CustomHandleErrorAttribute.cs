
using LFAdmin.Models.Model;
using LFAdmin.Services;
using Newtonsoft.Json;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LFAdmin.Views.Filters
{ 
    /// <summary>
    /// 异常处理filter
    /// </summary>
    public  class CustomHandleErrorAttribute :HandleErrorAttribute
    {
        ReturnData rd = new ReturnData();

        /// <summary>
        /// 会在程序出现异常时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if(!filterContext.ExceptionHandled) //检测异常是否被处理过
            {
                //filterContext.RouteData.Values["Action"]
                LoggerHelper.Error(httpContext.Session["JGLoginAccountID"].ToString()
                    , filterContext.RouteData.Values["Controller"].ToString()+"-"+filterContext.RouteData.Values["Action"].ToString()
                    , filterContext.Exception.Message, filterContext.Exception);
                if (httpContext.Request.IsAjaxRequest()) //如果是ajax异常,则返回json
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = filterContext.Exception.Message;
                    filterContext.Result = new JsonResult() {
                        Data = rd
                    }; 
                } 
                filterContext.ExceptionHandled = true;//处理完后设置为true

                //异常写入日志
                LoggerHelper.Error(filterContext.Exception.Message.ToString());

            }

        }
    }
}
