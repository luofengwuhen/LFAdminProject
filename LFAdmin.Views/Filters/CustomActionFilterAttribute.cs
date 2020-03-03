using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFAdmin.Views.Filters
{
    /// <summary>
    /// 1.可以通过写日志,监控action的执行耗时,性能监控
    /// 2.压缩返回的包大小
    /// 3.缓存 (利用header客户端缓存)
    /// </summary>
    public class CustomActionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if(filterContext.HttpContext.Request.Headers["Accept-Encoding"].Contains("deflate")) // 获取游览器的支持的格式
            {
                //如果支持压缩,则压缩
                filterContext.HttpContext.Response.AppendHeader("Accept-Encoding", "deflate");
                filterContext.HttpContext.Response.Filter = new DeflateStream
                    (filterContext.HttpContext.Response.Filter,CompressionMode.Compress);
            }
            else
            {
                Console.WriteLine("啥事不干...");
            }

            //可以写操作日志
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

    }
}