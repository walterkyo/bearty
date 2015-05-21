using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;

public partial class update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (Session["UserAuthentication"] == null)
		Response.Redirect("http://bearty.hk");
		if(Session["UserAuthentication"]!=null)
        {
            logout_lnk.Visible = true;
            login_btn.Text = Session["UserAuthentication"].ToString();
			updateLink.Visible = true;
			updateLink.NavigateUrl = "update.aspx";
            updateLink.Text = "Edit Info";
        }
    }

    protected void Submit_Btn_OnClick(object sender, EventArgs e)
    {
	if(cpwd.Text.ToString().Equals("") && !pwd.Text.ToString().Equals(""))
		ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('Please confirm the new password.');</script>");
	else{
		string username ="";
		string extension;
		string updateString;
		
		if (Session["UserAuthentication"] != null)
		username = (string)Session["UserAuthentication"];
		
		updateString = "update ba_user set ";
		if (pwd.Text.ToString().Length != 0)
			updateString = updateString + "password = '" + pwd.Text + "' ,";
		if (nickname.Text.ToString().Length != 0)
			updateString += "nickname = '" + nickname.Text + "' ,";
		if (email.Text.ToString().Length != 0)
			updateString = updateString + "email = '" + email.Text + "' ,";
		updateString = updateString.Substring(0,updateString.Length - 1);
		updateString += " WHERE login_name ='" + username + "'AND password = '" + opwd.Text + "'";
		//onn.Text = updateString;
		extension = null;
        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand updateSqlComm = default(OleDbCommand);
        updateSqlComm = new OleDbCommand(updateString, conn);

        int rowsAffected;
        rowsAffected = updateSqlComm.ExecuteNonQuery();
        
		
		if (!p_upload.PostedFile.FileName.ToString().Equals(""))
		{
			OleDbCommand searchSqlComm = default(OleDbCommand);
			OleDbCommand update2SqlComm = default(OleDbCommand);
			
			extension = Path.GetExtension(p_upload.PostedFile.FileName);
			searchSqlComm = new OleDbCommand("SELECT uid FROM ba_user WHERE login_name ='" + username + "'", conn);
			string img_name;
        	img_name = searchSqlComm.ExecuteScalar().ToString();
			
            string img_path = "/img/profile/" + img_name + extension;
			update2SqlComm = new OleDbCommand("update ba_user set img_path = '" + img_path + "' WHERE login_name ='" + username + "'", conn);
            update2SqlComm.ExecuteNonQuery();
			
			string strUri = "ftp://ftp.bearty.hk/web/img/profile/" + img_name + extension;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
            //request.Method = WebRequestMethods.Ftp.AppendFile;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = true;
            request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
            using (Stream s = request.GetRequestStream())
            {
                byte[] buffer = new byte[16 * 1024];
                int bytesRead;
                while ((bytesRead = p_upload.PostedFile.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    s.Write(buffer, 0, bytesRead);
                }
                //f_upload.PostedFile.InputStream.CopyTo(s);
                s.Flush();
            }
		}
		conn.Close();
	}
    }

    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/?category=" + sort_btn.Text);
    }

    protected void share_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("share.aspx");
    }

    protected void login_btn_OnClick(object sender, EventArgs e)
    {
        if (login_btn.Text.Equals("login"))
            Response.Redirect("login.aspx");
        else
            Response.Redirect("profile.aspx?user=" + Session["UserAuthentication"].ToString());
    }

    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if (!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/?search=" + search_bar.Text.ToString());
    }

    protected void RecentCMListView_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
        Label cm_content = (Label)e.Item.FindControl("cm_content");
        string cid = Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "cid"));
        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);
        selectSqlComm = new OleDbCommand("select comment FROM [post_cm] where [cid] = '" + cid + "'", conn);
        cm_content.Text = selectSqlComm.ExecuteScalar().ToString();
        conn.Close();

        HtmlImage detailCM_btn = (HtmlImage)e.Item.FindControl("detailCM_btn");
        HtmlImage simpleCM_btn = (HtmlImage)e.Item.FindControl("simpleCM_btn");
        detailCM_btn.Attributes.Add("onclick", "document.getElementById('" + cm_content.ClientID + "').style.display = '';document.getElementById('" + simpleCM_btn.ClientID + "').style.display = '';document.getElementById('" + detailCM_btn.ClientID + "').style.display = 'none';");
        simpleCM_btn.Attributes.Add("onclick", "document.getElementById('" + cm_content.ClientID + "').style.display = 'none';document.getElementById('" + simpleCM_btn.ClientID + "').style.display = 'none';document.getElementById('" + detailCM_btn.ClientID + "').style.display = '';");
    }

    protected void Calendar1_OnDayRender(Object source, DayRenderEventArgs e)
    {
        e.Day.IsSelectable = false;
        string eventYear = e.Day.Date.Year.ToString();
        string eventMonth = e.Day.Date.ToString("MMM");
        string eventDay = e.Day.Date.Day.ToString().PadLeft(2, '0');
        string sqlDate = eventYear + "-" + eventMonth + "-" + eventDay;

        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);

        selectSqlComm = new OleDbCommand("select count(*) as CRT FROM [ba_post] where [status] = 'approved' and event_date is not null and event_date like '%" + sqlDate + "%' and [p_type] like '%event%';", conn);
        int rowsAffected = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

        string eventList = "<font>Event date: " + e.Day.Date.ToString("dd/MMM") + "</font></br>";
        string title = "";
        if (rowsAffected > 0)
        {
            selectSqlComm = new OleDbCommand("select title,pid from ba_post where [status] = 'approved' and event_date is not null and event_date like '%" + sqlDate + "%' and [p_type] like '%event%' order by time;", conn);
            OleDbDataReader oleReader = selectSqlComm.ExecuteReader();
            while (oleReader.Read())
            {
                title = oleReader["title"].ToString();
                eventList += "<a href='postdetail.aspx?pid=" + oleReader["pid"].ToString() + "' class='event_link'>" + title + "</a></br>";
            }
            e.Cell.BackColor = System.Drawing.Color.Black;
            e.Cell.Text = "<font CLASS='calendar_thumbnail' >" + e.Day.Date.Day + "<span style='width:270px;'>" + eventList + "</span></font>";
        }
        conn.Close();

    }
	
	protected void logout_lnk_OnClick(object sender, EventArgs e) 
    {
        Session["UserAuthentication"] = null;
        Response.Redirect("http://bearty.hk");
    }

}