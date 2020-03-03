using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFAdmin.Views.Service;
namespace Service
{
    public class LoggerHelper
    {
        static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        static readonly log4net.ILog logmonitor = log4net.LogManager.GetLogger("logmonitor");

 
        public static void Error(string userid="", string modelname="", string ErrorMsg="", Exception ex = null)
        {
            if (ex != null)
            {
                logerror.Error("[UserID:"+ userid + ";Controller-Action:"+ modelname + "]:" +ErrorMsg, ex);
            }
            else
            {
                logerror.Error("[UserID:" + userid + ";Controller-Action:" + modelname + "]:" + ErrorMsg);
            }
        }

        public static void Info(string userid = "", string modelname = "", string Msg="")
        {
            loginfo.Info("[UserID:" + userid + ";Controller-Action:"+modelname + "]:" + Msg);
        }

  
    }
}