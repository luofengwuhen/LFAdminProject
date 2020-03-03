using LFAdmin.Models;
using LFAdmin.Models.Model;
using LFAdmin.Services;
using LFAdmin.Views.Service; 
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LFAdmin.Views.Controllers
{
    public class HomeController: BaseController
    {

        #region DI  依赖注入 
        //public ILoggerHelper LoggerHelper { get; }
        //public IBaseVariable _baseVariable { get; }
        //public HomeController(ILoggerHelper loggerHelper, IBaseVariable baseVariable)
        //{
        //    LoggerHelper = loggerHelper;
        //    _baseVariable = baseVariable;
        //}
        #endregion


        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {

            ViewBag.EmployeeName = Session["JGLoginAccountName"];

            int userid = Session["JGLoginAccountID"] != null ? int.Parse(Session["JGLoginAccountID"].ToString()) : 0;
         
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                List<string> ruleList = hee.V_User_Permission.Where(o => o.User_ID == userid).Select(o => o.Rule_Code).ToList(); 

                if (ruleList.Count > 0)
                {
                    ViewBag.RuleList = ruleList;
                }

            }
      

            return View();
        }
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
      [AllowAnonymous]
        public ActionResult Login()
        {
            Session["JGLoginAccount"] = null;
            Session["JGLoginAccountName"] = null;

            return View();
        }


        /// <summary>
        /// 登录 事件
        /// </summary>
        /// <returns></returns>
        //[LoginAccountAttribute(IsLogin = true)]
        [AllowAnonymous]
        public ActionResult LoginOn(string username, string pasword)
        {
             
                using (LFAdminEntities hee = new LFAdminEntities())
                {
                    //var emlist = hee.T_User.ToList();

                    var em = hee.T_User.Where(o => o.User_Name == username).FirstOrDefault();
                    if (em != null)
                    {
                        var em2 = hee.T_User.Where(o => o.User_Name == username && o.Password == "").FirstOrDefault();
                        if (em2 != null)
                        {
                            LoggerHelper.Info("账号 :" + username + ",首次登录,需先设置密码!"); 
                            rd.IsSuccess = "0";
                            rd.MessageString = "首次登录,需先设置密码,请联系管理员!";
                        return new JsonResult { Data = rd };
                    }
                        else
                        {
                            var em3 = hee.T_User.Where(o => o.User_Name == username && o.Password == pasword).FirstOrDefault();
                            if (em3 != null)
                            {
                                LoggerHelper.Info("账号 :" + username + ",登录成功!");
                                rd.IsSuccess = "1";
                                Session.Add("IsLogin", "True");//登录状态
                                Session.Add("JGLoginAccount", username);//账号
                                Session.Add("JGLoginAccountID", em3.ID);//ID
                                Session.Add("JGLoginAccountName", em3.Chinese_Name.ToString());//姓名
                                Session.Add("JGLoginCompanyName", em3.Company_Name.ToString());//公司名称
                                Session.Add("JGIsCompany", em3.Is_Company);//是否集团 

                            return new JsonResult { Data = rd };
                        }
                            else
                            {
                                LoggerHelper.Info("账号 :" + username + ",登录失败,密码不正确!"); 
                                rd.IsSuccess = "0";
                                rd.MessageString = "账号 : " + username + ",登录失败,密码不正确!";
                            return new JsonResult { Data = rd };
                        }

                        }
                    }
                    else
                    {
                        LoggerHelper.Info("账号 :" + username + " 不存在,登录失败!"); 
                        rd.IsSuccess = "0";
                        rd.MessageString = "账号 :" + username + " 不存在,登录失败!";
                        return new JsonResult { Data = rd };
                    }
                }
      
        }


    }
}
