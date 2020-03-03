using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace LFAdmin.Views.Service
{
    public class excelService
    {

        public DataSet ExcelToDS(string Path)
        {
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties='Excel 12.0 Xml;HDR=yes;IMEX=1;'";
            OleDbConnection conn = new OleDbConnection(strConn); 
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oada = new OleDbDataAdapter();

            conn.Open();
            cmd.Connection = conn;
            oada.SelectCommand = cmd; 

            cmd.CommandText = "SELECT * FROM [Sheet1$]";
            DataSet ds = new DataSet();
            oada.Fill(ds);

            return ds;
        }


        public void DSToExcel(string Path, DataSet oldds)
        {
            //先得到汇总EXCEL的DataSet 主要目的是获得EXCEL在DataSet中的结构 
            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source =" + Path + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(strCon);
            string strCom = "select * from [Sheet1$]";
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
            System.Data.OleDb.OleDbCommandBuilder builder = new OleDbCommandBuilder(myCommand);
            //QuotePrefix和QuoteSuffix主要是对builder生成InsertComment命令时使用。 
            builder.QuotePrefix = "[";     //获取insert语句中保留字符（起始位置） 
            builder.QuoteSuffix = "]"; //获取insert语句中保留字符（结束位置） 
            DataSet newds = new DataSet();
            myCommand.Fill(newds, "Table1");
            for (int i = 0; i < oldds.Tables[0].Rows.Count; i++)
            {
                //在这里不能使用ImportRow方法将一行导入到news中，因为ImportRow将保留原来DataRow的所有设置(DataRowState状态不变)。
                //在使用ImportRow后newds内有值，但不能更新到Excel中因为所有导入行的DataRowState != Added
            DataRow nrow = oldds.Tables["Table1"].NewRow();
                for (int j = 0; j < newds.Tables[0].Columns.Count; j++)
                {
                    nrow[j] = oldds.Tables[0].Rows[i][j];
                }
                newds.Tables["Table1"].Rows.Add(nrow);
            }
            myCommand.Update(newds, "Table1");
            myConn.Close();
        }
    }
}