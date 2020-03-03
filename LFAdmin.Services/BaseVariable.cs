
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFAdmin.Services
{
    public class BaseVariable 
    {
        public static string defaultPassword = "123";//默认密码
        public static string defaultCompany = "精工控股本级"; //本级公司 

        //string IBaseVariable.defaultPassword =>"123";//默认密码=> throw new NotImplementedException();

        //string IBaseVariable.defaultCompany => "精工控股本级";//throw new NotImplementedException();
    }

    
}