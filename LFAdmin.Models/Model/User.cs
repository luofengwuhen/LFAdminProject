using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFAdmin.Models.Model
{
    public partial class User
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string JGLoginAccount { get; set; }
        /// <summary>
        /// 账号ID
        /// </summary>
        public int JGLoginAccountID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string JGLoginAccountName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string JGLoginCompanyName { get; set; }

        /// <summary>
        /// 是否集团
        /// </summary>
        public bool JGIsCompany { get; set; }
    }
}