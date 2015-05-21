using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;

public partial class user_review : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["token"].Equals("token"))
        {
            if ((Request.QueryString["user"] != null))
                SqlDataSource1.SelectCommand = "SELECT * FROM [ba_user] where [login_name] = '" + Request.QueryString["user"] + "';";
        }
        else
        {
            Session["login_share"] = "y";
            Response.Redirect("http://bearty.hk");
        }
    }

    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/?category=" + sort_btn.Text);
    }

    protected void logout_lnk_OnClick(object sender, EventArgs e) 
    {
        Session["UserAuthentication"] = null;
        Response.Redirect("http://bearty.hk");
    }

    protected void rating_OnClick(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        //string rating = "";
        string rating_pid = "";

        if (btn.CommandName == "rating")
        {
            rating_pid = btn.CommandArgument.ToString();
            //string[] commandArgsAccept = btn.CommandArgument.ToString().Split(new char[] { ',' });
            //rating_pid = commandArgsAccept[0].ToString();
            //rating = commandArgsAccept[1].ToString();
        }

        //ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('" + rating_pid + "," + rating + "," + Session["UserAuthentication"] + "');</script>");

        if (Session["UserAuthentication"] != null)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            OleDbCommand selectSqlComm = default(OleDbCommand);
            selectSqlComm = new OleDbCommand("select count(*) as CRT FROM [post_rating] where [pid] = '" + rating_pid + "' and [login_name] = '" + Session["UserAuthentication"] + "'", conn);
            int rowsAffected = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

            if (rowsAffected >= 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('You can only rate this post once.');</script>");
            }
            else
            {
                OleDbCommand insertSqlComm = default(OleDbCommand);
                insertSqlComm = new OleDbCommand("insert into [post_rating]([pid], [login_name]) values('" + rating_pid + "','" + Session["UserAuthentication"] + "')", conn);
                insertSqlComm.ExecuteNonQuery();
                selectSqlComm = new OleDbCommand("select [total] FROM [ba_post] where [pid] = '" + rating_pid + "'", conn);
                int total = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

                /*selectSqlComm = new OleDbCommand("select [rating] FROM [ba_post] where [pid] = '" + rating_pid + "'", conn);
                string avgRating = selectSqlComm.ExecuteScalar().ToString();

                float temp = Convert.ToSingle(avgRating) * (float)total;
                temp += Convert.ToSingle(rating);
                

                float newRating = temp / (float)total;*/
                total += 1;
                insertSqlComm = new OleDbCommand("update [ba_post] set [total] = '" + total + "' where [pid] = '" + rating_pid + "'", conn);
                insertSqlComm.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('You have rated this post.');</script>");
            
            }
            conn.Close();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('Please login.');</script>");
        }
    }

    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if (!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/?search=" + search_bar.Text.ToString());
    }

    protected void search_user_btn_OnClick(object sender, EventArgs e)
    {
        if (!user_serach.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/user_review.aspx?token=token&user=" + user_serach.Text.ToString());
    }

    protected void share_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("share.aspx");
    }

    protected void login_btn_OnClick(object sender, EventArgs e)
    {
        if(login_btn.Text.Equals("login"))
            Response.Redirect("login.aspx");
        else
            Response.Redirect("profile.aspx?user=" + Session["UserAuthentication"].ToString());
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
            while(oleReader.Read()){
                title = oleReader["title"].ToString();
                //if (title.Length > 25)
                    //title = title.Substring(0, 25) + "…";
                eventList += "<a href='postdetail.aspx?pid=" + oleReader["pid"].ToString() + "' class='event_link'>" + title + "</a></br>";
            }
            e.Cell.BackColor = System.Drawing.Color.Black;
            e.Cell.Text = "<font CLASS='calendar_thumbnail' href=''>" + e.Day.Date.Day + "<span style='width:270px;'>" + eventList + "</span></font>";
            //e.Cell.Text = e.Day.Date.Day + "<span style='width:270px;'>" + eventList + "</span>";
            //e.Cell.CssClass = "calendar_thumbnail";
        }
        //else
        //{
            //ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('No event on this day.');</script>");
        //}
        conn.Close();

        //if (e.Day.Date.Day == 18)
        //e.Cell.Text = "<font CLASS='calendar_thumbnail' >" + e.Day.Date.Day + "<span style='width:270px'>" + eventList + "</span></font>";
        //e.Day.IsSelectable = false;
        // Change the background color of the days in the month
        // to yellow.
        //if (!e.Day.IsOtherMonth && !e.Day.IsWeekend)
            //e.Cell.BackColor = System.Drawing.Color.Yellow;
        /*
        Button btnTest = new Button();
        btnTest.ID = e.Day.ToString();   // you can change the id , the way you want it*/

        /*
        System.DateTime dDate = e.Day.Date;
        string sData = dDate.ToString("yyyy/MM/dd");

        Label dLB = new Label();
        dLB.ID = "LB_" + sData;
        dLB.Text = "[text]";
        e.Cell.Controls.Add(dLB);

        Panel dPL = new Panel();
        dPL.ID = "PL_" + sData;
        dPL.Controls.Add(String2Lit("X"));
        e.Cell.Controls.Add(dPL);

        string script = "Sys.Application.add_init(function() {" +
            "$create(AjaxControlToolkit.HoverMenuBehavior, {'dynamicServicePath':'','id':'" + sData + "','popupElement':$get('" + dPL.ID + "')}, null, null, $get('" + dLB.ID + "'));" +
        "});";
        ClientScript.RegisterStartupScript(Page.GetType(), sData, script, true);
        */

        /*
        AjaxControlToolkit.HoverMenuExtender dHover = new AjaxControlToolkit.HoverMenuExtender();
        dHover.TargetControlID = dLB.ID;
        dHover.PopupControlID = dPL.ID;
        dHover.OffsetX = 10;
        dHover.OffsetY = 10;
        dHover.PopDelay = 5;
        dHover.ID = "test1";

        e.Cell.Controls.Add(dHover);*/

        //HtmlGenericControl popUp = new HtmlGenericControl("table");
        //popUp.Attributes.Add("class", "dropv");
        //popUp.InnerHtml = "";
        // Add custom text to cell in the Calendar control.
        //if (e.Day.Date.Day == 18)
            //e.Cell.Controls.Add(popUp);
            //e.Cell.Controls.Add(btnTest);

    }

    /*public Literal String2Lit(string Pars)
    {
        Literal oLit = new Literal();

        oLit.Text = Pars;
        return oLit;
    }*/

    /*
    protected void Calendar1_OnSelectionChanged(object sender, EventArgs e)
    {
        Calendar calendar1 = (Calendar)sender;
        string eventYear = calendar1.SelectedDate.Year.ToString();
        string eventMonth = calendar1.SelectedDate.Month.ToString().PadLeft(2, '0');
        string eventDay = calendar1.SelectedDate.Day.ToString().PadLeft(2, '0');
        string sqlDate = eventYear + "-" + eventMonth + "-" + eventDay;

        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);

        selectSqlComm = new OleDbCommand("select count(*) as CRT FROM [ba_post] where [status] = 'approved' and CONVERT(VARCHAR(10),time,104) = '" + sqlDate + "' and [p_type] like '%event%';", conn);
        int rowsAffected = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

        if (rowsAffected > 0)
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM [ba_post] where [status] = 'approved' and CONVERT(VARCHAR(10),time,104) = '" + sqlDate + "' and [p_type] like '%event%' order by [time] desc;";
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('No event on this day.');</script>");
        }
        conn.Close();
    }*/

    protected void PostListView_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
   
    }
}