using HrAdminProject.Model;
using HrAdminProject.Models;
using HrAdminProject.Service;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace HrAdminProject.Controllers
{
    [LoginAccountAttribute(IsLogin = false)]
    public class GWPositionController : Controller
    {

        ReturnData rd = new ReturnData();
        ReturnList rl = new ReturnList();
        StaticService ss = new StaticService();
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns></returns>
        public string getList(int page, int limit, int Project_ID)
        {
            try
            {
                using (HrEvaEntities hee = new HrEvaEntities())
                {
                    //hee.Database.Log = Console.Write;//记录EF执行日志
                    List<GW_Position> queList = hee.GW_Position.ToList();

                    if (queList.Count > 0)
                    {
                     
                        queList = queList.Where(o => o.Project_ID==Project_ID).ToList(); 

                        rl.code = 0;
                        rl.msg = "";
                        int rowCount = queList.Count;
                        rl.count = rowCount;
                        //通过计算，得到分页应该需要分几页，其中不满一页的数据按一页计算
                        if (rowCount % limit != 0)
                        {
                            rowCount = rowCount / limit + 1;
                        }
                        else
                        {
                            rowCount = rowCount / limit;
                        }

                        queList = queList.Skip((page - 1) * limit).Take(limit).ToList();


                        rl.data = queList;
                        var strResult = JsonConvert.SerializeObject(rl);
                        return strResult;
                    }
                    else
                    {
                        rl.code = 1;
                        rl.msg = "查询结果为空!";
                        LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "GWPositionController-getList", rl.msg);
                        var strResult = JsonConvert.SerializeObject(rl);
                        return strResult;
                    }
                }
            }
            catch (Exception ex)
            {
                rl.code = 1;
                rl.msg = "查询数据失败 :" + ex.Message;
                LoggerHelper.Error(Session["JGLoginAccountID"].ToString(), "GWPositionController-getList", rl.msg, ex);
                var strResult = JsonConvert.SerializeObject(rl);
                return strResult;
            }

        }
    }
}