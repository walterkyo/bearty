<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {

        System.Timers.Timer aTimer = new System.Timers.Timer();

        aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);

        aTimer.Interval = 604800000;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;

        this.Application.Lock();
        this.Application["TimeStamp "] = DateTime.Now.ToString();
        this.Application.UnLock(); 

    }

    static void aTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            System.Data.OleDb.OleDbCommand deleteSqlComm = default(System.Data.OleDb.OleDbCommand);
            System.Data.OleDb.OleDbCommand selectSqlComm = default(System.Data.OleDb.OleDbCommand);
            System.Data.OleDb.OleDbCommand selectSqlComm2 = default(System.Data.OleDb.OleDbCommand);
            
            selectSqlComm = new System.Data.OleDb.OleDbCommand("select pid FROM [ba_post] where status='temporary'", conn);
            System.Data.OleDb.OleDbDataReader oleReader = selectSqlComm.ExecuteReader();
            while (oleReader.Read())
            {
                System.Data.OleDb.OleDbDataReader oleReader2;
                selectSqlComm2 = new System.Data.OleDb.OleDbCommand("select img_path from post_img where pid =" + oleReader["pid"] + "", conn);
                oleReader2 = selectSqlComm2.ExecuteReader();
                string img_path, img_name;
                while (oleReader2.Read())
                {
                    img_path = oleReader2["img_path"].ToString();
                    img_name = img_path.Replace("./post/img/", "");
                    string strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name;
                    System.Net.FtpWebRequest request = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(new Uri(strUri));
                    request.Method = System.Net.WebRequestMethods.Ftp.DeleteFile;
                    request.Timeout = (60000 * 1);
                    request.Credentials = new System.Net.NetworkCredential("bearty", "thFGmAKc");
                    System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)request.GetResponse();

                }
                deleteSqlComm = new System.Data.OleDb.OleDbCommand("delete from post_img where pid =" + oleReader["pid"] + "", conn);
                deleteSqlComm.ExecuteNonQuery();

                
                deleteSqlComm = new System.Data.OleDb.OleDbCommand("delete FROM [ba_post] where pid='" + oleReader["pid"] + "'", conn);
                deleteSqlComm.ExecuteNonQuery();
            }

            
            conn.Close();
        }
        catch (System.Data.OleDb.OleDbException ex)
        {
            throw new Exception(ex.Message);
        }  
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
