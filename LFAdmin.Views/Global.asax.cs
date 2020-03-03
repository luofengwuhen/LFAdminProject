
using LFAdmin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace LFAdmin.Views
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region DI  依赖注入 
        //public ILoggerHelper LoggerHelper { get; }
        //public MvcApplication(ILoggerHelper loggerHelper)
        //{
        //    LoggerHelper = loggerHelper;
        //}
        #endregion

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));
        }
        /// <summary>
        /// 只要web出现了没有处理的异常,最终都会进到这里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender,EventArgs e)
        {
            Exception error = Server.GetLastError();
            Console.WriteLine(error.Message);
            //异常写入日志
            LoggerHelper.Error(error.Message);

            Response.Write("Error");
            Response.ContentType = "text/html";

            Server.ClearError();//这样异常就不会暴露了

        }

    }
}
