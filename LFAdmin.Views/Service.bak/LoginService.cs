using LFAdmin.Models;
using LFAdmin.Views.Model; 
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFAdmin.Views.Service
{
    public class LoginService
    {
        ReturnData rd = new ReturnData();
        public string loginOn(string phone,string pasword)
        {
            //try
            //{
            //    using (LFAdminEntities hee = new LFAdminEntities())
            //    {
            //        var em = hee.T_Employee.Where(o => o.Phone == phone).FirstOrDefault();
            //        if (em != null)
            //        {
            //            var em2 = hee.T_Employee.Where(o => o.Phone == phone && o.Password == "").FirstOrDefault();
            //            if (em2 != null)
            //            {
            //                LoggerHelper.Info("账号 :" + phone + ",首次登录,需先设置密码!");
            //                rd.IsSuccess = "0";
            //                rd.MessageString = "首次登录,需先设置密码!";
            //                return JsonConvert.SerializeObject(rd);
            //            }
            //            else
            //            {
            //                var em3 = hee.T_Employee.Where(o => o.Phone == phone && o.Password == pasword).FirstOrDefault();
            //                if (em3 != null)
            //                {

            //                    LoggerHelper.Info("手机号 :" + phone + ",登录成功!");


            //                    rd.MessageString = em3.Employee_Name.ToString();
            //                    rd.IsSuccess = "1";
            //                    return JsonConvert.SerializeObject(rd);
            //                }
            //                else
            //                {
            //                    LoggerHelper.Info("账号 :" + phone + ",登录失败,密码不正确!");
            //                    rd.IsSuccess = "0";
            //                    rd.MessageString = "账号 : " + phone + ",登录失败,密码不正确!";
            //                    return JsonConvert.SerializeObject(rd);
            //                }

            //            }
            //        }
            //        else
            //        {
            //            LoggerHelper.Info("账号 :" + phone + " 不存在,登录失败!");
            //            rd.IsSuccess = "0";
            //            rd.MessageString = "账号 :" + phone + " 不存在,登录失败!";
            //            return JsonConvert.SerializeObject(rd);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LoggerHelper.Error("账号 :" + phone + ",登录失败!" + ex.Message);
            //    rd.IsSuccess = "0";
            //    rd.MessageString = "账号 :" + phone + ",登录失败!" + ex.Message;
            //    return JsonConvert.SerializeObject(rd);
            //}
            return null;
        }
    }
}