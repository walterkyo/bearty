using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class postdetail_cn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null)
        {
            logout_lnk.Visible = true;
            login_btn.Text = Session["UserAuthentication"].ToString(); ;
        }

        if (!IsPostBack)
        {
            if ((Request.QueryString["pid"] != null))
            {
                if (!Request.QueryString["pid"].ToString().Equals(""))
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    conn.Open();
                    OleDbCommand selectSqlComm = default(OleDbCommand);
                    selectSqlComm = new OleDbCommand("select count(*) as CRT from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);
                    int rowsAffected = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

                    if (rowsAffected > 0)
                    {
                        Literal p_content = (Literal)this.FindControl("p_content");
                        SqlDataSource2.SelectCommand = "SELECT * FROM [post_cm] where pid = '" + Request.QueryString["pid"] + "' order by [time] desc;";

                        selectSqlComm = new OleDbCommand("select * from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);

                        OleDbDataReader oleReader = default(OleDbDataReader);
                        oleReader = selectSqlComm.ExecuteReader();
                        oleReader.Read();

                        p_content.Text += "<div class='lb'>";
                        p_content.Text += "<div style='width:764px;word-wrap:break-word;overflow: auto;color:white;font-family:Arial;'>" + oleReader["title"] + "</div>";
                        p_content.Text += "</div>";
                        p_content.Text += "<A rel='lightbox' HREF='" + oleReader["p_img"] + "'><img src='" + oleReader["p_img"] + "' width = '100%' id='featured-image'/></A> </br><div style='padding:10px;font-family:Adobe 繁黑體 Std B;font-size:small;-webkit-text-size-adjust:none;'>";
                        //p_content.Text += "<img src='" + oleReader["p_img"] + "' width = '100%'/> </br><div style='padding:10px;'>";

                        HtmlMeta meta = new HtmlMeta();
                        meta.Attributes.Add("property", "og:title");
                        meta.Content = oleReader["title"].ToString();
                        this.Header.Controls.Add(meta);

                        meta = new HtmlMeta();
                        meta.Attributes.Add("property", "og:image");

                        String imgMeta = oleReader["p_img"].ToString();
                        imgMeta = imgMeta.Replace("./post/img/", "");
                        meta.Content = "http://www.bearty.hk/post/img/" + imgMeta;
                        this.Header.Controls.Add(meta);


                        meta = new HtmlMeta();
                        meta.Attributes.Add("property", "og:url");
                        meta.Content = "http://bearty.hk/postdetail_cn.aspx?pid=" + Request.QueryString["pid"].ToString();
                        this.Header.Controls.Add(meta);

                        string filePath = oleReader["p_content"].ToString();

                        oleReader.Close();

                        string inputString, desString;
                        desString = "";
                        int i = 0;

                        try
                        {
                            using (StreamReader streamReader = new StreamReader(Server.MapPath("~") + @filePath, System.Text.Encoding.UTF8))
                            {
                                inputString = streamReader.ReadLine();
                                while (inputString != null)
                                {
                                    p_content.Text += inputString;
                                    if (i == 0)
                                        desString = inputString;
                                    inputString = streamReader.ReadLine();
                                    i++;
                                }
                            }
                        }
                        catch { Response.Redirect("error.aspx?code=1"); }

                        meta = new HtmlMeta();
                        meta.Attributes.Add("property", "og:description");
						string fbdes = Regex.Replace(desString, @"<(.|\n)*?>", string.Empty);

						meta.Content = fbdes + "...";
						this.Header.Controls.Add(meta);
						
                        p_content.Text += "</div>";

                        p_content.Text += "</br><div style='float:right;padding-right:5px;'>";
                        selectSqlComm = new OleDbCommand("select time from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);
                        string dp_time = selectSqlComm.ExecuteScalar().ToString();
                        selectSqlComm = new OleDbCommand("select login_name from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);
                        string dp_author = selectSqlComm.ExecuteScalar().ToString();
                        p_content.Text += "Author: <a href='profile.aspx?user=" + dp_author + "' class='b_link' style='text-align:left;'>" + dp_author + "</a>&emsp;";
                        p_content.Text += dp_time;

                        selectSqlComm = new OleDbCommand("select related_post from ba_post where pid = '" + Request.QueryString["pid"].ToString() + "'", conn);
                        string restr = selectSqlComm.ExecuteScalar().ToString();
                        string setitle = "";
                        if (!restr.Equals(""))
                        {
                            string[] works = restr.Split(';');
                            foreach (string work in works)
                            {
                                if (work.Length > 0)
                                {
                                    try
                                    {
                                        restr = work.Trim().Replace(";", "");

                                        if (restr.StartsWith("http://bearty.hk") || restr.StartsWith("bearty.hk") || restr.StartsWith("http://www.bearty.hk") || restr.StartsWith("www.bearty.hk"))
                                        {
                                            restr = restr.Replace("http://bearty.hk/postdetail.aspx?pid=", "");
                                            restr = restr.Replace("bearty.hk/postdetail.aspx?pid=", "");
                                            restr = restr.Replace("www.bearty.hk/postdetail.aspx?pid=", "");
                                            restr = restr.Replace("http://www.bearty.hk/postdetail.aspx?pid=", "");
                                            restr = restr.Replace("http://bearty.hk/postdetail_cn.aspx?pid=", "");
                                            restr = restr.Replace("bearty.hk/postdetail_cn.aspx?pid=", "");
                                            restr = restr.Replace("www.bearty.hk/postdetail_cn.aspx?pid=", "");
                                            restr = restr.Replace("http://www.bearty.hk/postdetail_cn.aspx?pid=", "");
                                            restr = restr.Replace("aspx", "");

                                            selectSqlComm = new OleDbCommand("select title from ba_post where pid = '" + restr + "'", conn);
                                            if (selectSqlComm.ExecuteScalar() != null)
                                            {
                                                setitle = selectSqlComm.ExecuteScalar().ToString();
                                                relink.Text += "<a href='postdetail.aspx?pid=" + restr + "' class='event_link'>" + setitle + "</a></br>";
                                                relb.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            selectSqlComm = new OleDbCommand("select * from ba_post where title like '%" + restr + "%'", conn);
                                            OleDbDataReader oleReader2 = selectSqlComm.ExecuteReader();
                                            while (oleReader2.Read())
                                            {
                                                if (!oleReader2["title"].ToString().Equals(""))
                                                {
                                                    if (!oleReader2["pid"].ToString().Equals(Request.QueryString["pid"]))
                                                    {
                                                        relink.Text += "<a href='postdetail.aspx?pid=" + oleReader2["pid"].ToString() + "' class='event_link'>" + oleReader2["title"].ToString() + "</a></br>";
                                                        relb.Visible = true;
                                                    }
                                                }
                                            }
                                            oleReader2.Close();
                                        }
                                    }
                                    catch (OleDbException exx) { }
                                }
                            }
                        }

                        selectSqlComm = new OleDbCommand("select total from ba_post where pid ='" + Request.QueryString["pid"].ToString() + "'", conn);
                        string rating = selectSqlComm.ExecuteScalar().ToString();
                        int irating = Convert.ToInt32(rating);
                        int level = irating / 10;

                        Button ratingButton = (Button)FindControl("rate");


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

                        conn.Close();
                    }
                    else
                        Response.Redirect("error.aspx?code=1");
                }else
                    Response.Redirect("http://bearty.hk/post_cn.aspx");
            }
            else
            {
                Response.Redirect("http://bearty.hk/post_cn.aspx");
            }
        }

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

    protected void share_btn_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("share_cn.aspx");
    }

    protected void login_btn_OnClick(object sender, EventArgs e)
    {
        if (login_btn.Text.Equals("登入"))
            Response.Redirect("login_cn.aspx");
        else
            Response.Redirect("profile_cn.aspx?user=" + Session["UserAuthentication"].ToString());
    }

    protected void rating_OnClick(object sender, EventArgs e)
    {

        //ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('" + rating_pid + "," + rating + "," + Session["UserAuthentication"] + "');</script>");

        if (Session["UserAuthentication"] != null)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            OleDbCommand selectSqlComm = default(OleDbCommand);
            selectSqlComm = new OleDbCommand("select count(*) as CRT FROM [post_rating] where [pid] = '" + Request.QueryString["pid"].ToString() + "' and [login_name] = '" + Session["UserAuthentication"] + "'", conn);
            int rowsAffected = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

            if (rowsAffected >= 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('You can only rate this post once.');</script>");
            }
            else
            {
                OleDbCommand insertSqlComm = default(OleDbCommand);
                insertSqlComm = new OleDbCommand("insert into [post_rating]([pid], [login_name]) values('" + Request.QueryString["pid"].ToString() + "','" + Session["UserAuthentication"] + "')", conn);
                insertSqlComm.ExecuteNonQuery();
                selectSqlComm = new OleDbCommand("select [total] FROM [ba_post] where [pid] = '" + Request.QueryString["pid"].ToString() + "'", conn);
                int total = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());

                /*selectSqlComm = new OleDbCommand("select [rating] FROM [ba_post] where [pid] = '" + rating_pid + "'", conn);
                string avgRating = selectSqlComm.ExecuteScalar().ToString();

                float temp = Convert.ToSingle(avgRating) * (float)total;
                temp += Convert.ToSingle(rating);
                

                float newRating = temp / (float)total;*/
                total += 1;
                insertSqlComm = new OleDbCommand("update [ba_post] set [total] = '" + total + "' where [pid] = '" + Request.QueryString["pid"].ToString() + "'", conn);
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

    protected void del_btn_OnClick(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            string author = "";
            OleDbCommand selectSqlComm = default(OleDbCommand);
            selectSqlComm = new OleDbCommand("select login_name from ba_post where pid =" + pid + "", conn);
            author = selectSqlComm.ExecuteScalar().ToString();

            if (author.Equals(Session["UserAuthentication"].ToString()))
            {
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
                Response.Redirect("http://bearty.hk/post_cn.aspx");
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('You are not the author of this post.')</script>");
                conn.Close();
            }
        }else
            Response.Write(@"<script language='javascript'>alert('Please login.')</script>");
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


        author_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

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

        Response.Redirect("postdetail_cn.aspx?pid=" + Request.QueryString["pid"] + "#comments");
    }
}
