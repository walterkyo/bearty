using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;

public partial class logincn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		//if (Session["UserAuthentication"] != null)
		//Response.Redirect("http://bearty.hk");
        if (Session["UserAuthentication"] != null)
        {
            logout_lnk.Visible = true;
            login_btn.Text = Session["UserAuthentication"].ToString(); ;
        }

        if (Session["login_share"] == null)
            Session["login_share"] = "n";

        if (!Session["login_share"].ToString().Equals("y"))
            Session["login_share"] = "n";
    }

    protected void logout_lnk_OnClick(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Response.Redirect("post_cn.aspx");
    }

    protected void Register_Btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("register_cn.aspx");
    }

    protected void Submit_Btn_OnClick(object sender, EventArgs e)
    {
        string username = login_name.Text;
		string pwd = password.Text;
        //string admin ;

        if (!username.Trim().Equals("") && !pwd.Trim().Equals(""))
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            OleDbCommand insertSqlComm = default(OleDbCommand);
            insertSqlComm = new OleDbCommand("SELECT login_name FROM ba_user WHERE login_name ='" + username + "' AND password ='" + pwd + "'", conn);

            string CurrentName;
            CurrentName = (String)insertSqlComm.ExecuteScalar();
            insertSqlComm = new OleDbCommand("SELECT status FROM ba_user WHERE login_name ='" + username + "' AND password ='" + pwd + "'", conn);
            string CurrStatus = (String)insertSqlComm.ExecuteScalar();

            conn.Close();

            if (CurrentName != null)
            {

                if (CurrStatus.Equals("admin"))
                {
                    Session["admin"] = "admin";
                    Session.Timeout = 15;
                    Session["UserAuthentication"] = username;
                    Response.Redirect("http://bearty.hk/admin.aspx");
                }
                else
                {
                    Session["admin"] = null;
                    Session["UserAuthentication"] = username;
                    Session.Timeout = 15;
                    try
                    {
                        if (!Session["login_share"].ToString().Equals(""))
                        {
                            if (Session["login_share"].ToString().Equals("y"))
                            {
                                Session["login_share"] = "n";
                                Response.Redirect("http://bearty.hk/share_cn.aspx");
                            }
                            else
                                Response.Redirect("http://bearty.hk/post_cn.aspx");
                        }
                        else
                            Response.Redirect("http://bearty.hk/post_cn.aspx");
                    }
                    catch (Exception exxx) { Response.Redirect("http://bearty.hk/post_cn.aspx"); }
                }

            }
            else
            {
                Session["UserAuthentication"] = null;
                errormsg.Text = "Invalid Username or Password";
            }
        }
    }

    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/post_cn.aspx?category=" + sort_btn.ID.ToString());
    }

    protected void share_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("share_cn.aspx");
    }

    protected void login_btn_OnClick(object sender, EventArgs e)
    {
        if (login_btn.Text.Equals("µn¤J"))
            Response.Redirect("login_cn.aspx");
        else
            Response.Redirect("profile_cn.aspx?user=" + Session["UserAuthentication"].ToString());
    }

    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if (!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/post_cn.aspx?search=" + search_bar.Text.ToString());
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

                eventList += "<a href='postdetail_cn.aspx?pid=" + oleReader["pid"].ToString() + "' class='event_link'>" + title + "</a></br>";
            }
            e.Cell.BackColor = System.Drawing.Color.Black;
            e.Cell.Text = "<font CLASS='calendar_thumbnail' >" + e.Day.Date.Day + "<span style='width:270px;'>" + eventList + "</span></font>";
        }

        conn.Close();


    }



}// C# Document