using LFAdmin.Views.Controllers; 
using System.Web;
using System.Web.Mvc;
using LFAdmin.Views.Filters;
using LFAdmin.Services;

namespace LFAdmin.Views
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute()); //判断是否登录action
            //filters.Add(new LoginAccountAttribute()); //判断是否登录action
            filters.Add(new CustomHandleErrorAttribute()); //错误拦截action
        }
    }
}
