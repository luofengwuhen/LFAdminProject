using LFAdmin.Models.Model;
using LFAdmin.Services; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LFAdmin.Views.Controllers
{
    public class BaseController : Controller
    {
        public ReturnData rd = new ReturnData();
        public ReturnList rl = new ReturnList(); 
     

    }
}