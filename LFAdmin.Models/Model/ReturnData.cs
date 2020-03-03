using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFAdmin.Models.Model
{
    public class ReturnData
    {
        public string IsSuccess;
        public string MessageString;
    }


    public class ReturnList
    {
        public int code;
        public string msg;
        public int count;
        public object data;
    }

    public class TreeData
    {
        public int code;
        public string msg;
        public data data { get; set; }
    }

    public class data
    {
        public List<Nodes> list { get; set; }
        public List<string> checkedAlias;
        public List<string> disabledAlias; 
    }
    public class Nodes
    {
        public long id;
        public string name;
        public string alias;
        public string palias;  
    };

 
}