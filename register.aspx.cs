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
using System.Text.RegularExpressions;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(Session["UserAuthentication"]!=null)
        {
            Response.Redirect("http://bearty.hk");
        };
    }

    public static bool IsEmailAdress(string sEmail)
    {
        if (sEmail != "")
        {
            Regex sRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return sRegex.IsMatch(sEmail) ? true : false;
        }
        else
        {
            return false;
        }
    }

    protected void Submit_Btn_OnClick(object sender, EventArgs e)
    {
        Boolean flag_duplicate = false;
        if (login_name.Text.Equals("") || nickname.Text.Equals(""))
            msgbox.Text = "●Fill in login name/nickname";
        else if (pwd.Text.Equals(""))
            msgbox.Text = "●Fill in password";
        else if (pwd.Text.Length < 8 || pwd.Text.Length>16)
            msgbox.Text = "●Password length should be within 8-16";
        else if (!pwd.Text.Equals(cpwd.Text))
            msgbox.Text = "●Mistyping in password";
        else if(!IsEmailAdress(email.Text))
            msgbox.Text = "●Invalid email";
        else
        {
            //check if name exists already
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            OleDbCommand cmd = default(OleDbCommand);
            cmd = new OleDbCommand("SELECT login_name FROM ba_user WHERE login_name = '" + login_name.Text + "'", conn);
            //cmd.Parameters.AddWithValue("@Name", login_name.Text);
            string dr = (String)cmd.ExecuteScalar();
            conn.Close();
            if (dr != null)
            {
                msgbox.Text = "●Login name has already taken";
            }
            else
            {
                string extension = Path.GetExtension(p_upload.PostedFile.FileName);
                string chkExtension = extension.ToLower();
                Boolean extensionFlag = false;
                Boolean filesizeFlag = true;

                if (p_upload.PostedFile.FileName.ToString().Equals(""))
                {
                    conn = new OleDbConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    conn.Open();
                    OleDbCommand insertSqlComm = default(OleDbCommand);
                    insertSqlComm = new OleDbCommand("INSERT INTO ba_user(password,login_name,status,nickname,email,img_path) VALUES('" + pwd.Text + "','" + login_name.Text + "','In progress','" + nickname.Text + "','" + email.Text + "','/img/profile/default_profile.png')", conn);

                    int rowsAffected;
                    rowsAffected = insertSqlComm.ExecuteNonQuery();

                    conn.Close();
                    Session["UserAuthentication"] = login_name.Text;
                    Session.Timeout = 15;
                    Response.Redirect("http://bearty.hk/registered.aspx");
                }
                else
                {
                    if (chkExtension.Equals(".jpg") || chkExtension.Equals(".jpeg") || chkExtension.Equals(".png"))
                        extensionFlag = true;

                    if (p_upload.PostedFile.ContentLength > 2097152)
                        filesizeFlag = false;

                    if (extensionFlag && filesizeFlag)
                    {
                        OleDbCommand searchSqlComm = default(OleDbCommand);
                        OleDbCommand updateSqlComm = default(OleDbCommand);

                        extension = Path.GetExtension(p_upload.PostedFile.FileName);

                        conn = new OleDbConnection();
                        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        conn.Open();
                        OleDbCommand insertSqlComm = default(OleDbCommand);
                        insertSqlComm = new OleDbCommand("INSERT INTO ba_user(password,login_name,status,nickname,email,img_path) VALUES('" + pwd.Text + "','" + login_name.Text + "','In progress','" + nickname.Text + "','" + email.Text + "','/img/profile/default_profile.png');SELECT SCOPE_IDENTITY() AS 'identity'", conn);


                        //searchSqlComm = new OleDbCommand("SELECT uid FROM ba_user WHERE login_name ='" + login_name.Text + "'", conn);
                        string img_name;
                        img_name = insertSqlComm.ExecuteScalar().ToString();

                        string img_path = "/img/profile/" + img_name + extension;
                        updateSqlComm = new OleDbCommand("update ba_user set img_path = '" + @img_path + "' WHERE uid ='" + img_name + "'", conn);
                        updateSqlComm.ExecuteNonQuery();

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
                        conn.Close();
                        Session["UserAuthentication"] = login_name.Text;
                        Session.Timeout = 15;
                        Response.Redirect("http://bearty.hk/registered.aspx");
                    }
                    else
                    { 
                        if(!extensionFlag)
                            msgbox.Text = "Invalid file type";
                        if(!filesizeFlag)
                            msgbox.Text = "File size exceeds 2MB";
                    }
                    
                }
                
            }
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