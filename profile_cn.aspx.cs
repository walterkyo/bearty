using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;

public partial class profile_cn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(Session["UserAuthentication"]!=null)
        {
            logout_lnk.Visible = true;
            login_btn.Text = Session["UserAuthentication"].ToString(); ;
			
        }
		
		String username="abc";
		Boolean self;
		if (Request.QueryString["user"] != null){
			username = Request.QueryString["user"];
		}
		else if (Session["UserAuthentication"] != null)
			username = Session["UserAuthentication"].ToString();
		else Response.Redirect("http://bearty.hk");
		
		if(Session["UserAuthentication"]!=null){
		if 	(username.Equals(Session["UserAuthentication"].ToString())){
			update.Visible = true;
			update.NavigateUrl = "update.aspx";
            update.Text = "Edit Info";
		}
		}
		
		uname.Text = username;
		
		protfolioLink.NavigateUrl = "profile.aspx?category=original&user="+username;
		allLink.NavigateUrl = "profile.aspx?user="+username;
			
		String imgpath = "";
		OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);
        selectSqlComm = new OleDbCommand("select img_path FROM [ba_user] where [login_name] = '" + Request.QueryString["user"] + "'", conn);
        try{imgpath = selectSqlComm.ExecuteScalar().ToString().Trim();}
		catch{Response.Redirect("http://bearty.hk");}
		if(imgpath == null){
			Response.Redirect("http://bearty.hk");
		}
        conn.Close();
		profilepic.ImageUrl = imgpath;
		
		if(imgpath != null){
		
		if ((Request.QueryString["user"] !=null)){
			SqlDataSource1.SelectCommand = "SELECT * FROM [ba_post] where [status] = 'approved' and [login_name] = '"+ Request.QueryString["user"] +"' order by [time] desc;";
        if ((Request.QueryString["category"] != null))
            SqlDataSource1.SelectCommand = "SELECT * FROM [ba_post] where [status] = 'approved' and [login_name] = '"+ Request.QueryString["user"] +"' and [p_type] like N'%" + Request.QueryString["category"] + "%' order by [time] desc;";
        else if ((Request.QueryString["search"] != null))
            SqlDataSource1.SelectCommand = "SELECT * FROM [ba_post] where [status] = 'approved' and [login_name] = '"+ Request.QueryString["user"] +"' and ([title] like N'%" + Request.QueryString["search"] + "%' or [login_name] = N'" + Request.QueryString["search"] + "') order by [time] desc;";
        else if ((Request.QueryString["event"] != null))
            SqlDataSource1.SelectCommand = "SELECT * FROM [ba_post] where [status] = 'approved' and [login_name] = '"+ Request.QueryString["user"] +"' and CONVERT(VARCHAR(10),time,104) = '" + Request.QueryString["event"] + "' and [p_type] like '%event%' order by [time] desc;";}}
    }

    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/post_cn.aspx?category=" + sort_btn.ID.ToString());
    }

    protected void logout_lnk_OnClick(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Response.Redirect("http://bearty.hk/post_cn.aspx");
    }

    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if(!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/post_cn.aspx?search=" + search_bar.Text.ToString());
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

    protected void PostListView_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem listViewDataItem = e.Item as ListViewDataItem;
        string filePath = Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "p_content"));
        Literal content = (Literal)e.Item.FindControl("p_content");

        string inputString;

        try
        {
            using (StreamReader streamReader = new StreamReader(Server.MapPath("~") + @filePath, System.Text.Encoding.UTF8))
            {
                inputString = streamReader.ReadLine();
                while (inputString != null && content.Text.Length < 200)
                {
                    //content.Text += inputString + "<//br>";
                    string txt = Regex.Replace(inputString, "<(.|\n)*?>", "");
                    content.Text += txt;
                    inputString = streamReader.ReadLine();
                }
            }
        }
        catch { }

        //System.Net.WebUtility.HtmlDecode(content.Text);
        System.Web.HttpUtility.HtmlDecode(content.Text);
        if (content.Text.Length > 200)
            content.Text = content.Text.Substring(0, 200) + "...";

        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);

        selectSqlComm = new OleDbCommand("select count(*) as CRT from post_cm where pid ='" + Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "pid")) + "'", conn);
        Label cm_counter = (Label)e.Item.FindControl("cm_counter");
        cm_counter.Text = selectSqlComm.ExecuteScalar().ToString();

        //string cm_counter = selectSqlComm.ExecuteScalar().ToString();

        content.Text = content.Text.ToString().Replace("&nbsp;", " ");

        string catagory = null;
        selectSqlComm = new OleDbCommand("select p_type from ba_post where pid ='" + Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "pid")) + "'", conn);
        catagory = selectSqlComm.ExecuteScalar().ToString();

        Label catagory_lb = (Label)e.Item.FindControl("catagory_lb");
        catagory_lb.Text = catagory.Replace(";", "&nbsp;&nbsp;");

        selectSqlComm = new OleDbCommand("select total from ba_post where pid ='" + Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "pid")) + "'", conn);
        string rating = selectSqlComm.ExecuteScalar().ToString();
        int irating = Convert.ToInt32(rating);
        int level = irating / 10;

        Button ratingButton = (Button)e.Item.FindControl("rate");
        /*
        ratingButton.Style.Clear();
        ratingButton.Style.Add("left", "90%");
        ratingButton.Style.Add("position", "absolute");
        ratingButton.Style.Add("bottom", "2.7%");
        ratingButton.Style.Add("width", "22px");
        ratingButton.Style.Add("height", "21px");
        ratingButton.Style.Add("font-size", "12px");*/

        switch (level)
        {
            case 0:
                ratingButton.Style.Add("width", "22px");
                ratingButton.Style.Add("height", "21px");
                ratingButton.Style.Add("font-size", "12px");
                break;
            case 1:
                ratingButton.Style.Add("width", "25px");
                ratingButton.Style.Add("height", "24px");
                ratingButton.Style.Add("font-size", "12px");
                break;
            case 2:
                ratingButton.Style.Add("width", "28px");
                ratingButton.Style.Add("height", "27px");
                ratingButton.Style.Add("font-size", "13px");
                break;
            case 3:
                ratingButton.Style.Add("width", "31px");
                ratingButton.Style.Add("height", "30px");
                ratingButton.Style.Add("font-size", "14px");
                break;
            case 4:
                ratingButton.Style.Add("width", "34px");
                ratingButton.Style.Add("height", "33px");
                ratingButton.Style.Add("font-size", "15px");
                break;
            case 5:
                ratingButton.Style.Add("width", "37px");
                ratingButton.Style.Add("height", "36px");
                ratingButton.Style.Add("font-size", "16px");
                break;
            case 6:
                ratingButton.Style.Add("width", "40px");
                ratingButton.Style.Add("height", "39px");
                ratingButton.Style.Add("font-size", "17px");
                break;
            case 7:
                ratingButton.Style.Add("width", "43px");
                ratingButton.Style.Add("height", "42px");
                ratingButton.Style.Add("font-size", "18px");
                break;
            case 8:
                ratingButton.Style.Add("width", "44px");
                ratingButton.Style.Add("height", "43px");
                ratingButton.Style.Add("font-size", "19px");
                break;
            case 9:
                ratingButton.Style.Add("width", "47px");
                ratingButton.Style.Add("height", "46px");
                ratingButton.Style.Add("font-size", "20px");
                break;
            default:
                ratingButton.Style.Add("width", "50px");
                ratingButton.Style.Add("height", "49px");
                ratingButton.Style.Add("font-size", "21px");
                break;
        }

        ratingButton.Style.Add("-webkit-text-size-adjust", "none");
        /*
        Literal bot_bar = (Literal)e.Item.FindControl("bot_bar");
        bot_bar.Text += "<span style='position:relative;display:block;height:auto;'>";
        bot_bar.Text += "<div class='fb-like' style='position:absolute;left:70%;bottom:0%;' data-href='http://bearty.hk/postdetail.aspx?pid=" + Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "pid")) + "' data-send='false' data-layout='button_count' data-width='450' data-show-faces='true' ></div>";
        bot_bar.Text += "<span class='bubble' style='left:85%;bottom:50%;background: #DCDCDC; border-color: #DCDCDC;'><asp:Label id='cm_counter' runat='server' style='-webkit-text-size-adjust:none;font-size:12px;font-weight:bold;' ForeColor='Black' text='1'></asp:Label></span>";
        bot_bar.Text += "</span>";*/
        /*
           selectSqlComm = new OleDbCommand("select rating from ba_post where pid ='" + Convert.ToString(DataBinder.Eval(listViewDataItem.DataItem, "pid")) + "'", conn);
           string rating = selectSqlComm.ExecuteScalar().ToString();
           float frating = Convert.ToSingle(rating);
           int irating = (int)frating;
           for (int i = 1; i <= irating; i++)
           {
               string rateControlName = "rate" + i;
               Button ratingButton = (Button)e.Item.FindControl(rateControlName);
               ratingButton.Style.Add("background", "#FF0000");
               ratingButton.Style.Add("border", "1px solid #FF0000");
               ratingButton.Style.Add("color", "#FFFFFF");
           }*/
        conn.Close();
    }

}