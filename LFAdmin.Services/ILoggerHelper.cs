using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAdmin.Services
{
    public interface ILoggerHelper
    {
         void Error(string userid = "", string modelname = "", string ErrorMsg = "", Exception ex = null);
        void Error(string ErrorMsg = "");
        void Info(string userid = "", string modelname = "", string Msg = "");

        void Info(string Msg = "");

    }
}
