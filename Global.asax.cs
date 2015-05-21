using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Data.OleDb;

namespace qumiao.com
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //定義定時器 
            System.Timers.Timer myTimer = new System.Timers.Timer(86400000);
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
        }
        void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            YourTask();
        }
        void YourTask()
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            OleDbCommand deleteSqlComm = default(OleDbCommand);
            deleteSqlComm = new OleDbCommand("delete FROM [ba_post] where status='temporary'", conn);
            deleteSqlComm.ExecuteNonQuery();
            conn.Close();
        }
        protected void Application_End(object sender, EventArgs e)
        {

            //下面的代碼是關鍵，可解決IIS應用程序池自動回收的問題
            Thread.Sleep(1000);
            //這裏設置你的web地址，可以隨便指向你的任意一個aspx頁面甚至不存在的頁面，目的是要激發Application_Start
            //string url = "http://www.qumiao.com";手機主題
            string url = "error.aspx";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回寫的字節流

        }
    }
}