﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;

public partial class apostdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] != null)
        {
            if (Session["UserAuthentication"] != null)
            {
                login_lnk.Visible = false;
                logout_lnk.Visible = true;
                username.Visible = true;
                username.NavigateUrl = "profile.aspx?user=" + Session["UserAuthentication"].ToString();
                username.Text = Session["UserAuthentication"].ToString();
            }

            if (!IsPostBack)
            {
                if ((Request.QueryString["pid"] != null))
                {
                    Literal p_content = (Literal)this.FindControl("p_content");
                    SqlDataSource2.SelectCommand = "SELECT * FROM [post_cm] where pid = '" + Request.QueryString["pid"] + "' order by [time] asc;";

                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    conn.Open();
                    OleDbCommand selectSqlComm = default(OleDbCommand);
                    selectSqlComm = new OleDbCommand("select * from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);
                    OleDbDataReader oleReader = default(OleDbDataReader);
                    oleReader = selectSqlComm.ExecuteReader();
                    oleReader.Read();

                    p_content.Text += "<img src='" + oleReader["p_img"] + "' width = '100%'/> </br><hr/>";
                    string filePath = oleReader["p_content"].ToString();

                    oleReader.Close();
                    conn.Close();

                    string inputString;

                    try
                    {
                        using (StreamReader streamReader = new StreamReader(Server.MapPath("~") + @filePath, System.Text.Encoding.UTF8))
                        {
                            inputString = streamReader.ReadLine();
                            while (inputString != null)
                            {
                                p_content.Text += inputString;
                                inputString = streamReader.ReadLine();
                            }
                        }
                    }
                    catch { Response.Redirect("error.aspx"); }
                }
                else
                {
                    Response.Redirect("post.aspx");
                }
            }
        }
        else
            Response.Redirect("http://bearty.hk");
    }


    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/admin.aspx?category=" + sort_btn.Text);
    }

    protected void logout_lnk_OnClick(object sender, EventArgs e)
    {
        Session["admin"] = null;
        Session["UserAuthentication"] = null;
        Response.Redirect("http://bearty.hk");
    }

    protected void share_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("share.aspx");
    }

    protected void shop_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("shop.aspx");
    }

    protected void del_btn_OnClick(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            OleDbCommand selectSqlComm = default(OleDbCommand);

            OleDbCommand deleteSqlComm = default(OleDbCommand);
            deleteSqlComm = new OleDbCommand("delete from post_cm where pid =" + pid + "", conn);
            deleteSqlComm.ExecuteNonQuery();

            string strUri = "ftp://ftp.bearty.hk/web/post/content/" + pid + ".txt";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Timeout = (60000 * 1);
            request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            deleteSqlComm = new OleDbCommand("delete from ba_post where pid =" + pid + "", conn);
            deleteSqlComm.ExecuteNonQuery();

            OleDbDataReader oleReader;
            selectSqlComm = new OleDbCommand("select img_path from post_img where pid =" + pid + "", conn);
            oleReader = selectSqlComm.ExecuteReader();

            string img_path, img_name;
            while (oleReader.Read())
            {
                img_path = oleReader["img_path"].ToString();
                img_name = img_path.Replace("./post/img/", "");
                strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name;
                request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Timeout = (60000 * 1);
                request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                response = (FtpWebResponse)request.GetResponse();
            }

            deleteSqlComm = new OleDbCommand("delete from post_img where pid =" + pid + "", conn);
            deleteSqlComm.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("http://bearty.hk/admin.aspx");

        }
        else
            Response.Write(@"<script language='javascript'>alert('Please login.')</script>");
    }

    protected void del_cm_btn_OnClick(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null)
        {

            Button btn = (Button)sender;
            //string rating = "";
            string cid = "";

            if (btn.CommandName == "del_cm")
            {
                cid = btn.CommandArgument.ToString();
                //string[] commandArgsAccept = btn.CommandArgument.ToString().Split(new char[] { ',' });
                //rating_pid = commandArgsAccept[0].ToString();
                //rating = commandArgsAccept[1].ToString();
            }

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            int pid = Convert.ToInt32(Request.QueryString["pid"]);

            OleDbCommand deleteSqlComm = default(OleDbCommand);
            deleteSqlComm = new OleDbCommand("delete from post_cm where cid =" + cid + "", conn);
            deleteSqlComm.ExecuteNonQuery();

            conn.Close();
            Response.Redirect("http://bearty.hk/apostdetail.aspx?pid=" + pid + "#comments");

        }
        else
            Response.Write(@"<script language='javascript'>alert('Please login.')</script>");
    }

    protected void appr_btn_OnClick2(object sender, EventArgs e)
    {
        if (Session["admin"] != null && Session["admin"].ToString().Equals("admin"))
        {
            Response.Redirect("admin.aspx");

        }
        else
        {
            Response.Write(@"<script language='javascript'>alert('You are not the administrater.')</script>");
        }


    }

    protected void appr_btn_OnClick(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null)
        {

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();
                int pid = Convert.ToInt32(Request.QueryString["pid"]);
                //string admin = "";
                //OleDbCommand selectSqlComm = default(OleDbCommand);
                //selectSqlComm = new OleDbCommand("select login_name from ba_post where status = admin", conn);
                //admin = selectSqlComm.ExecuteScalar().ToString();

                if (Session["admin"]!= null && Session["admin"].ToString().Equals("admin"))
                {
                    OleDbCommand approveSqlComm = default(OleDbCommand);
                    //approveSqlComm = new OleDbCommand("update post_cm where pid =" + pid + "", conn);
                    //approveSqlComm.ExecuteNonQuery();

                    /*string strUri = "ftp://ftp.bearty.hk/web/post/content/" + pid + ".txt";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    request.Timeout = (60000 * 1);
                    request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();*/



                    /*OleDbDataReader oleReader;
                    selectSqlComm = new OleDbCommand("select img_path from post_img where pid =" + pid + "", conn);
                    oleReader = selectSqlComm.ExecuteReader();

                    string img_path, img_name;
                    while (oleReader.Read())
                    {
                        img_path = oleReader["img_path"].ToString();
                        img_name = img_path.Replace("./post/img/", "");
                        strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name;
                        request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                        request.Method = WebRequestMethods.Ftp.DeleteFile;
                        request.Timeout = (60000 * 1);
                        request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                        response = (FtpWebResponse)request.GetResponse();
                    }*/

                    //deleteSqlComm = new OleDbCommand("delete from post_img where pid =" + pid + "", conn);
                    //deleteSqlComm.ExecuteNonQuery();

                    approveSqlComm = new OleDbCommand("update ba_post set status = 'approved' where pid =" + pid + "", conn);
                    approveSqlComm.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("http://bearty.hk/admin.aspx");
                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('You are not the administer.')</script>");
                    conn.Close();
                }

        }else
                Response.Write(@"<script language='javascript'>alert('Please login.')</script>");
    }


    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if (!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/admin.aspx?search=" + search_bar.Text.ToString());
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

                eventList += "<a href='apostdetail.aspx?pid=" + oleReader["pid"].ToString() + "' class='event_link'>" + title + "</a></br>";
            }
            e.Cell.BackColor = System.Drawing.Color.Black;
            e.Cell.Text = "<font CLASS='calendar_thumbnail' >" + e.Day.Date.Day + "<span style='width:270px;'>" + eventList + "</span></font>";
        }

        conn.Close();


    }

    protected void cm_btn_OnClick(object sender, EventArgs e)
    {
        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);
        OleDbCommand insertSqlComm = default(OleDbCommand);
        //OleDbDataReader oleReader = default(OleDbDataReader);
        string author;

        if (Session["UserAuthentication"] == null)
            author = "anonymous";
        else
            author = Session["UserAuthentication"].ToString();

        string author_IP="";


        author_IP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();

        string cm = post_cm.Text.Replace(Environment.NewLine,"<br/>");

        if(cm.Length == 0)
        {
            Response.Write(@"<script language='javascript'>alert('Your comment cannot be empty.')</script>");
        }else
        {
            selectSqlComm = new OleDbCommand("select title from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'",conn);
            string post_title = selectSqlComm.ExecuteScalar().ToString();

            insertSqlComm = new OleDbCommand("INSERT INTO post_cm(time,pid,comment,author,author_IP, title) VALUES('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + Request.QueryString["pid"].ToString() + "',N'" + cm + "','" + author + "','" + author_IP + "',N'" + post_title + "')", conn);
            insertSqlComm.ExecuteNonQuery();
        }
        conn.Close();

        Response.Redirect("apostdetail.aspx?pid=" + Request.QueryString["pid"]);
    }

}
