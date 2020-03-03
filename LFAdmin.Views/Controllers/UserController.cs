using LFAdmin.Models;
using LFAdmin.Models.Model;
using LFAdmin.Views.Service;
using Newtonsoft.Json;
using LFAdmin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LFAdmin.Repositories;

namespace LFAdmin.Views.Controllers
{

    //[LoginAccountAttribute(IsLogin = false)]
    public class UserController : BaseController
    {
        LFAdminEntities hee = new LFAdminEntities();
        //ISysUserRepository userRepository = new SysUserRepository(new LFAdminEntities());

        #region DI  依赖注入 
        //public ILoggerHelper LoggerHelper { get; }
        //public IBaseVariable BaseVariable { get; }
        //public UserController(ILoggerHelper loggerHelper, IBaseVariable baseVariable)
        //{
        //    LoggerHelper = loggerHelper;
        //    BaseVariable = baseVariable;
        //}
        #endregion


        // GET: Employee
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //using (LFAdminEntities hee = new LFAdminEntities())
            
            {
                var comList = hee.T_Company.ToList();
                //var comList=userRepository.getUser();
                ViewBag.comList = comList;
                if (comList.Count > 0)
                {
                    string companyName = comList.FirstOrDefault().Company_Name;
                    if (!companyName.Equals(""))
                    {
                        var depList = hee.T_Department.Where(o => o.Company_Name.Equals(companyName))
                            .ToList();
                        ViewBag.depList = depList;
                    }
                }
            }
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
     
            return View();
        }

        public ActionResult Add2()
        {
            //int j = 0;
            //int i = 8 / j;


            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var comList = hee.T_Company.ToList();
                ViewBag.comList = comList;

                if (comList.Count > 0)
                {
                    string companyName = comList.FirstOrDefault().Company_Name;
                    if (!companyName.Equals(""))
                    {
                        var depList = hee.T_Department.Where(o => o.Company_Name.Equals(companyName))
                            .ToList();
                        ViewBag.depList = depList;
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// 编辑查询
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Edit2(int ID)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                {
                    ViewBag.ID = em.ID;
                    ViewBag.User_Name = em.User_Name;
                    ViewBag.Chinese_Name = em.Chinese_Name;
                    ViewBag.Company_Name = em.Company_Name;
                    ViewBag.Department_Name = em.Department_Name;
                    ViewBag.Position_Name = em.Position_Name;

                    
                    var comList = hee.T_Company.ToList();
                    ViewBag.comList = comList;

                    if (comList.Count > 0)
                    {
                        string companyName = comList.FirstOrDefault().Company_Name;
                        if (!companyName.Equals(""))
                        {
                            var depList = hee.T_Department.Where(o => o.Company_Name.Equals(companyName))
                                .ToList();
                            ViewBag.depList = depList;
                        }
                    } 

                }
            } 
            return View();
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Phone"></param>
        /// <param name="Employee_Name"></param>
        /// <param name="Company_Name"></param>
        /// <param name="Department_Name"></param>
        /// <param name="Position_Name"></param>
        /// <returns></returns>
        public ActionResult SaveInfo2(int ID, string User_Name, string Chinese_Name, string Company_Name, string Department_Name, string Position_Name)
        { 
          
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                {
                    em.User_Name = User_Name;
                    em.Chinese_Name = Chinese_Name;
                    em.Company_Name = Company_Name;
                    em.Department_Name = Department_Name;
                    em.Position_Name = Position_Name;

                    hee.SaveChanges();

                    rd.IsSuccess = "1";
                    rd.MessageString = "信息修改成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SaveInfo2", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "查询账户 :" + User_Name + "信息时出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SaveInfo2", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            } 

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Phone"></param>
        /// <param name="Employee_Name"></param>
        /// <param name="Company_Name"></param>
        /// <param name="Department_Name"></param>
        /// <param name="Position_Name"></param>
        /// <returns></returns>
        public ActionResult AddInfo(string User_Name, string Chinese_Name, string Company_Name, string Department_Name, string Position_Name)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            { 
                T_User em = new T_User();
                em.User_Name = User_Name;
                em.Chinese_Name = Chinese_Name;
                em.Company_Name = Company_Name;
                em.Department_Name = Department_Name;
                em.Position_Name = Position_Name;
                em.Password = BaseVariable.defaultPassword;
                em.Is_Company = false;
                hee.T_User.Add(em);
                hee.SaveChanges();

                rd.IsSuccess = "1";
                rd.MessageString = "信息新增成功!";
                LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-AddInfo", rd.MessageString);
                return new JsonResult { Data = rd };
            } 

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DelInfo(int ID)
        {
            ReturnData rd = new ReturnData();
 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                {
                    hee.T_User.Remove(em);
                    hee.SaveChanges();
                    rd.IsSuccess = "1";
                    rd.MessageString = "信息删除成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-DelInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "信息删除出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-DelInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            } 

        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ResetInfo(int ID)
        {  
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.ID == ID).FirstOrDefault();
                if (em != null)
                {
                    em.Password = BaseVariable.defaultPassword;
                    hee.SaveChanges();
                    rd.IsSuccess = "1";
                    rd.MessageString = "密码重置为" + em.Password + "成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-ResetInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "密码重置出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-ResetInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            } 

        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns></returns>
        public string getList(int page, int limit, string company, string depertment, string employee)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var emList = hee.T_User.ToList();
                if (emList.Count > 0)
                {
                    if (!string.IsNullOrEmpty(company))
                    {
                        emList = emList.Where(o => o.Company_Name.Contains(company)).ToList();
                    }
                    if (!string.IsNullOrEmpty(depertment))
                    {
                        emList = emList.Where(o => o.Department_Name.Contains(depertment)).ToList();
                    }

                    if (!string.IsNullOrEmpty(employee))
                    {
                        emList = emList.Where(o => o.Chinese_Name.Contains(employee)).ToList();
                    }


                    rl.code = 0;
                    rl.msg = "";
                    int rowCount = emList.Count;
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

                    emList = emList.Skip((page - 1) * limit).Take(limit).ToList();


                    rl.data = emList;
                    var strResult = JsonConvert.SerializeObject(rl);
                    return strResult;
                    //return Json(strResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    rl.code = 1;
                    rl.msg = "查询结果为空!";
                    LoggerHelper.Error(Session["JGLoginAccountID"].ToString(), "UserController-getList", rl.msg);
                    var strResult = JsonConvert.SerializeObject(rl);
                    return strResult;
                    //return Json(strResult, JsonRequestBehavior.AllowGet);
                }
            }
            

        }





        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查看 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string username = Session["JGLoginAccount"] != null ? Session["JGLoginAccount"].ToString() : "";
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.UserName = username;


                using (LFAdminEntities hee = new LFAdminEntities())
                {
                    var em = hee.T_User.Where(o => o.User_Name == username).FirstOrDefault();
                    if (em != null)
                    {
                        ViewBag.Chinese_Name = em.Chinese_Name;
                        ViewBag.Company_Name = em.Company_Name;
                        ViewBag.Department_Name = em.Department_Name;
                        ViewBag.Position_Name = em.Position_Name;
                    }
                }
            }

            return View();
        }


        //修改个人信息
        public ActionResult SaveInfo(string username, string employeename, string Company_Name, string Department_Name, string positionname)
        {
       
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.User_Name == username).FirstOrDefault();
                if (em != null)
                {
                    em.Chinese_Name = employeename;
                    em.Company_Name = Company_Name;
                    em.Department_Name = Department_Name;
                    em.Position_Name = positionname;

                    hee.SaveChanges();

                    rd.IsSuccess = "1";
                    rd.MessageString = "信息修改成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SaveInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "查询账号 :" + username + "信息时出错!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SaveInfo", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            } 

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="employeename"></param>
        /// <param name="companyname"></param>
        /// <param name="depertmentname"></param>
        /// <param name="positionname"></param>
        /// <returns></returns>
        public ActionResult SavePassword(string username, string password1, string password2)
        { 
            
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var em = hee.T_User.Where(o => o.User_Name == username && o.Password == password1).FirstOrDefault();
                if (em != null)
                {
                    em.Password = password2;

                    hee.SaveChanges();

                    rd.IsSuccess = "1";
                    rd.MessageString = "密码修改成功!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SavePassword", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
                else
                {
                    rd.IsSuccess = "0";
                    rd.MessageString = "账号 :" + username + "的密码不正确!";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SavePassword", rd.MessageString);
                    return new JsonResult { Data = rd };
                }
            }
             

        }
        public string uploadUser(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            ReturnList rl = new ReturnList();
            string FileName;
            string savePath;
            rl.code = 0;
            rl.msg = "上传成功";
            if (file == null || file.ContentLength <= 0)
            {
                rl.code = 1;
                rl.msg= "文件不能为空";
                return JsonConvert.SerializeObject(rl);
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    rl.code = 1;
                    rl.msg = "文件类型不对，只能导入xls和xlsx格式的文件";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-uploadUser", rl.msg);
                    return JsonConvert.SerializeObject(rl);
                }
                if (filesize >= Maxsize)
                {
                    rl.code = 1;
                    rl.msg = "上传文件超过4M，不能上传";
                    LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-uploadUser", rl.msg);
                    return JsonConvert.SerializeObject(rl);
                }
            }
                return JsonConvert.SerializeObject(rl);
        }



        public ActionResult AddRole(int ID)
        {
             
            using (LFAdminEntities hee = new LFAdminEntities())
            {
 
                ViewBag.ID = ID; 

                var roleList = hee.T_Role.ToList();
                if (roleList .Count()>0)
                {
                    ViewBag.RoleList = roleList;
                }
            } 
            return View();
        }

        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <param name="Role_Name">角色名称</param>
        /// <param name="Role_ID">角色ID</param>
        /// <returns></returns>
        public ActionResult SaveRole(int ID,int Role_ID)
        { 
            using (LFAdminEntities hee = new LFAdminEntities())
            {
                var turList = hee.T_User_Role.Where(o => o.User_ID == ID).ToList();
                if(turList.Count>0)
                {
                    hee.T_User_Role.RemoveRange(turList); 
                }

                //保存用户角色
                T_User_Role ur = new T_User_Role();
                ur.Role_ID = Role_ID;
                ur.User_ID = ID; 
                hee.T_User_Role.Add(ur);

                //保存用户角色名称
                var user = hee.T_User.Where(o => o.ID == ID).FirstOrDefault();
                if(user!=null)
                {
                    var ro = hee.T_Role.Where(o => o.ID == Role_ID).FirstOrDefault();
                    if(ro!=null)
                    {
                        user.Role_Name = ro.Role_Name;
                    }
                      
                }

                //保存用户权限
                var rpList= hee.T_Role_Permission.Where(o => o.Role_ID == Role_ID).ToList();
                if(rpList.Count()>0)
                {
                    var upList = hee.T_User_Permission.Where(o => o.User_ID == ID).ToList();
                    if(upList.Count()>0)
                    {
                        hee.T_User_Permission.RemoveRange(upList); 
                    }
                    foreach (var rp in rpList)
                    {
                        T_User_Permission up = new T_User_Permission();
                        up.User_ID = ID;
                        up.Rule_Code = rp.Permission_Code;
                        hee.T_User_Permission.Add(up);
                    }
                }

                hee.SaveChanges();
                rd.IsSuccess = "1";
                rd.MessageString = "权限保存成功!";
                LoggerHelper.Info(Session["JGLoginAccountID"].ToString(), "UserController-SaveRole", rd.MessageString);
                return new JsonResult { Data = rd };
            }

             

        }


    }
}