using LFAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFAdmin.Views.Services
{
    public class getListService
    {
        public string getList<T>(int page, int limit, Object obj)
        {
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                
                //var emList = hee.obj.ToList();
                //if (emList.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(company))
                //    {
                //        emList = emList.Where(o => o.Company_Name.Contains(company)).ToList();
                //    }
                //    if (!string.IsNullOrEmpty(depertment))
                //    {
                //        emList = emList.Where(o => o.Department_Name.Contains(depertment)).ToList();
                //    }

                //    if (!string.IsNullOrEmpty(employee))
                //    {
                //        emList = emList.Where(o => o.Chinese_Name.Contains(employee)).ToList();
                //    }


                //    rl.code = 0;
                //    rl.msg = "";
                //    int rowCount = emList.Count;
                //    rl.count = rowCount;
                //    //通过计算，得到分页应该需要分几页，其中不满一页的数据按一页计算
                //    if (rowCount % limit != 0)
                //    {
                //        rowCount = rowCount / limit + 1;
                //    }
                //    else
                //    {
                //        rowCount = rowCount / limit;
                //    }

                //    emList = emList.Skip((page - 1) * limit).Take(limit).ToList();


                //    rl.data = emList;
                //    var strResult = JsonConvert.SerializeObject(rl);
                //    return strResult;
                //    //return Json(strResult, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    rl.code = 1;
                //    rl.msg = "查询结果为空!";
                //    LoggerHelper.Error(Session["JGLoginAccountID"].ToString(), "UserController-getList", rl.msg);
                //    var strResult = JsonConvert.SerializeObject(rl);
                //    return strResult;
                //    //return Json(strResult, JsonRequestBehavior.AllowGet);
                //}

                return null;
            }


        }

    }
}