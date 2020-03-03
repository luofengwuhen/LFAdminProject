using LFAdmin.Models;
using LFAdmin.Models.Model;
using LFAdmin.Views.Service;
using Newtonsoft.Json;
using LFAdmin.Services;
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

namespace LFAdmin.Views.Controllers
{ 
    public class RoleController : BaseController
    {

        #region DI  依赖注入 
        //public ILoggerHelper LoggerHelper { get; }
        //public IBaseVariable _baseVariable { get; }
        //public RoleController(ILoggerHelper loggerHelper, IBaseVariable baseVariable)
        //{
        //    LoggerHelper = loggerHelper;
        //    _baseVariable = baseVariable;
        //}
        #endregion

        #region  列表

        public ActionResult List()
        {
            return View();
        }


        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns></returns>
        public string getList(int page, int limit, string Department, string Organization_Type)
        {
            try
            {
                using (LFAdminEntities hee = new LFAdminEntities())
                {
                    var queList = hee.T_Role.ToList();

                    if (queList.Count > 0)
                    { 
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
                        LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-getList", rl.msg);
                        var strResult = JsonConvert.SerializeObject(rl);
                        return strResult;
                    }
                }
            }
            catch (Exception ex)
            {
                rl.code = 1;
                rl.msg = "查询数据失败 :" + ex.Message;
                LoggerHelper.Error(Session["JGLoginAccountID"].ToString(), "RoleController-getList", rl.msg,ex);
                var strResult = JsonConvert.SerializeObject(rl);
                return strResult;
            }

        }
        #endregion



        #region 新增

        /// <summary>
        /// 360
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary> 
        /// <returns></returns>
        public ActionResult AddInfo(string Role_Code, string Role_Name)
        {
             
                using (LFAdminEntities hee = new LFAdminEntities())
                {

                    var ro = hee.T_Role.Where(o => o.Role_Code == Role_Code).FirstOrDefault();
                    if(ro==null)
                    {
                        T_Role tc = new T_Role();
                        tc.Role_Code = Role_Code;
                        tc.Role_Name = Role_Name;

                        hee.T_Role.Add(tc);
                        hee.SaveChanges();


                        rd.IsSuccess = "1";
                        rd.MessageString = "信息新增成功!";
                        LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-AddInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                    else
                    {
                        rd.IsSuccess = "0";
                        rd.MessageString = "角色编码已存在!";
                        LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-AddInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }

              
                }
            

        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑打开
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Edit(int ID)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var tc = hee.T_Role.Where(o => o.ID == ID).FirstOrDefault();
                if (tc != null)
                {
                    ViewBag.Role = tc;
                }
            } 

            return View();
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Questionnaire_Name"></param>
        /// <returns></returns>
        public ActionResult SaveInfo(int ID, string Role_Code, string Role_Name)
        {
            
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_Role.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                { 
                    em.Role_Code = Role_Code;
                    em.Role_Name = Role_Name;
                    hee.SaveChanges();

                    rd.IsSuccess = "1";
                    rd.MessageString = "信息修改成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-SaveInfo", rd.MessageString);
                    return new JsonResult { Data = rd };

                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "信息修改出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-SaveInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            }
            
        }

        #endregion


        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DelInfo(int ID)
        { 
             
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_Role.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                {
                    var urList = hee.T_User_Role.Where(o => o.Role_ID == ID).ToList();
                    if(urList.Count>0)
                    {
                        rd.IsSuccess = "0";
                        rd.MessageString = "该角色已绑定用户,请解绑后删除!";
                        LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-DelInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }

                    hee.T_Role.Remove(em);
                    hee.SaveChanges();
                    rd.IsSuccess = "1";
                    rd.MessageString = "信息删除成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-DelInfo", rd.MessageString);
                return new JsonResult { Data = rd };

            }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "信息删除出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-DelInfo", rd.MessageString);
                return new JsonResult { Data = rd };
            }
            }
             

        }
        #endregion

        #region 角色权限

        public ActionResult RolePermission(int ID)
        {
          
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var tc = hee.T_Role.Where(o => o.ID == ID).FirstOrDefault();
                if (tc != null)
                {
                    ViewBag.Role = tc;
                }
            } 

            return View();
        }

        public ActionResult SaveRolePermission(int ID,string authids)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var rpList= hee.T_Role_Permission.Where(o => o.Role_ID == ID).ToList();
                if(rpList.Count>0)
                {
                    hee.T_Role_Permission.RemoveRange(rpList);
                } 

                var peCode = authids.Split(new char[1] { ',' });
                foreach(var code in peCode)
                {
                    T_Role_Permission rp = new T_Role_Permission();
                    rp.Permission_Code =  code;
                    rp.Role_ID = ID;
                    hee.T_Role_Permission.Add(rp);
                }
                hee.SaveChanges();
                rd.IsSuccess = "1";
                rd.MessageString = "权限保存成功!";
                LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "RoleController-RolePermission", rd.MessageString);
                return new JsonResult { Data = rd };
            } 
        }

        /// <summary>
        /// 获取权限tree
        /// </summary>
        /// <returns></returns>
        public ActionResult getTreeData(int ID)
        {
            TreeData td = new TreeData();
            td.code = 0;
            td.msg = "获取成功"; 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                data dt = new data();
                List<Nodes> NodesList = new List<Nodes>();

           
                var tpList = hee.T_Permission.ToList();
                foreach(var tp in tpList)
                {
                    Nodes nd = new Nodes();
                    nd.id = tp.ID;
                    nd.name = tp.name;
                    nd.alias = tp.alias;
                    nd.palias = tp.palias;
                    NodesList.Add(nd); 
                }

                var trpList = hee.T_Role_Permission.Where(o => o.Role_ID == ID).ToList();
                List<string> aliasList = new List<string>();
                foreach (var tp in NodesList)
                {
                    if(trpList.Where(o=>o.Permission_Code==tp.alias).FirstOrDefault()!=null)
                    {
                        aliasList.Add(tp.alias);
                    }
                } 
                dt.checkedAlias = aliasList;
                //dt.disabledAlias = li;
                dt.list = NodesList;
                td.data = dt;
            }

            return new JsonResult { Data = td };
        }



        #endregion

    }
}