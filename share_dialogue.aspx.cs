using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class share_dialogue : System.Web.UI.Page
{
    int pid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] != null && (Request.QueryString["token"].Equals("token")))
        {
            if (!IsPostBack)
            {
                Session["Image"] = "";
                Session["img_path"] = null;
                Session["cover_counter"] = 0;
                Session["pid"] = "";
                Session["eDate"] = null;
                Session["cover_Image"] = null;
                for (int i = 1; i < 10; i++)
                {
                    Session[i + "_id"] = null;
                    Session[i + "_img"] = null;
                    Session[i + "_html_img"] = null;
                }

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();
                OleDbCommand insertSqlComm = default(OleDbCommand);

                insertSqlComm = new OleDbCommand("INSERT INTO ba_post(time,p_content,title,status,p_type,login_name,p_img,total) VALUES('" + DateTime.Now.ToString("yyyy/MMM/dd HH:mm:ss") + "','0','0','temporary','0','" + Session["UserAuthentication"] + "','0','0');SELECT SCOPE_IDENTITY() AS 'identity'", conn);
                pid = Convert.ToInt32(insertSqlComm.ExecuteScalar().ToString());
                Session["pid"] = pid;

                conn.Close();

                string sUrl = "Calendar.aspx?TextBoxId=" + eventDate_tb.ClientID;
                string sScript = "var x = screen.availWidth/2;var y = screen.availHeight/2;window.open('" + sUrl + "','','height=270,width=265,status=no,toolbar=no,menubar=no,location=no,left=' + (x-50) + ',top=' + y + ',scrollbars=no,resizable=no','')";
                eventDate_tb.Attributes.Add("onclick", sScript);

                logout_lnk.Visible = true;
                login_btn.Text = Session["UserAuthentication"].ToString();
            }

            if (Session["eDate"] != null)
                eventDate_tb.Text = Session["eDate"].ToString();
        }
        else
        {
            Session["login_share"] = "y";
            Response.Redirect("login.aspx");
        }
    }

    protected void sort_btn_OnClick(object sender, EventArgs e)
    {
        Button sort_btn = (Button)sender;
        Response.Redirect("http://bearty.hk/?category=" + sort_btn.Text);
    }

    protected void login_btn_OnClick(object sender, EventArgs e)
    {
        if (login_btn.Text.Equals("login"))
            Response.Redirect("login.aspx");
        else
            Response.Redirect("profile.aspx?user=" + Session["UserAuthentication"].ToString());
    }

    protected void logout_lnk_OnClick(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Response.Redirect("http://bearty.hk");
    }

    protected void yt_btn_OnClick(object sender, EventArgs e)
    {
        string yt_link = yt.Text;
        yt_link = yt_link.Replace("http://www.youtube.com/watch?v=", "");
        yt_link = yt_link.Replace("www.youtube.com/watch?v=", "");
        yt_link = yt_link.Replace("youtube.com/watch?v=", "");
        Boolean long_yt_link = yt_link.Contains("&");
        if (long_yt_link)
        {
            int temp = yt_link.IndexOf("&");
            yt_link = yt_link.Remove(temp);
        }

        yt_content.Text += "[youtube]" + yt_link + "[/youtube]";
    }

    protected void search_btn_OnClick(object sender, EventArgs e)
    {
        if (!search_bar.Text.ToString().Trim().Equals(""))
            Response.Redirect("http://bearty.hk/?search=" + search_bar.Text.ToString());
    }

    protected void uploadmore_btn_OnClick(object sender, EventArgs e)
    {
        uploadmore_panel.Visible = true;
        uploadmore_btn.Visible = false;
    }

    protected void clear_btn_OnClick(object sender, EventArgs e)
    {
        OleDbConnection conn = new OleDbConnection();
        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        conn.Open();
        OleDbCommand selectSqlComm = default(OleDbCommand);
        OleDbCommand deleteSqlComm = default(OleDbCommand);
        OleDbDataReader oleReader;
        selectSqlComm = new OleDbCommand("select img_path from post_img where pid ='" + Session["pid"] + "'", conn);
        oleReader = selectSqlComm.ExecuteReader();

        string img_path, img_name;
        while (oleReader.Read())
        {
            img_path = oleReader["img_path"].ToString();
            img_name = img_path.Replace("./post/img/", "");
            string strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Timeout = (60000 * 1);
            request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        }

        deleteSqlComm = new OleDbCommand("delete from post_img where pid ='" + Session["pid"] + "'", conn);
        deleteSqlComm.ExecuteNonQuery();

        deleteSqlComm = new OleDbCommand("delete from ba_post where pid ='" + Session["pid"] + "'", conn);
        deleteSqlComm.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("share.aspx");
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

    protected void upload_btn_OnClick(object sender, EventArgs e)
    {
        if (!f_upload.PostedFile.FileName.ToString().Equals(""))
        {
            if (upload_btn.Text.ToString().Equals("change"))
            {
                OleDbConnection chg_conn = new OleDbConnection();
                chg_conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                chg_conn.Open();
                OleDbCommand chg_deleteSqlComm = default(OleDbCommand);

                Session["cover_image"] = "";

                string chg_img_path, chg_img_name;
                chg_img_path = Session["img_path"].ToString();
                chg_img_name = chg_img_path.Replace("./post/img/", "");
                string strUri = "ftp://ftp.bearty.hk/web/post/img/" + chg_img_name;

                FtpWebRequest chg_request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                chg_request.Method = WebRequestMethods.Ftp.DeleteFile;
                chg_request.Timeout = (60000 * 1);
                chg_request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                FtpWebResponse chg_response = (FtpWebResponse)chg_request.GetResponse();


                chg_deleteSqlComm = new OleDbCommand("delete from post_img where img_id =" + Session["cover_img_id"] + "", chg_conn);
                chg_deleteSqlComm.ExecuteNonQuery();
                chg_conn.Close();
            }


            string extension = Path.GetExtension(f_upload.PostedFile.FileName);
            string chkExtension = extension.ToLower();
            Boolean extensionFlag = false;
            Boolean filesizeFlag = true;

            if (chkExtension.Equals(".jpg") || chkExtension.Equals(".jpeg") || chkExtension.Equals(".png"))
                extensionFlag = true;

            if (f_upload.PostedFile.ContentLength > 4194305)
                filesizeFlag = false;

            if (extensionFlag && filesizeFlag)
            {
                int fileLength = Convert.ToInt32(f_upload.PostedFile.InputStream.Length);
                byte[] imageBuffer = new byte[fileLength];
                f_upload.PostedFile.InputStream.Read(imageBuffer, 0, fileLength);
                MemoryStream imageMS = new MemoryStream(imageBuffer);

                System.Drawing.Image inputImage = System.Drawing.Image.FromStream(imageMS);

                ImageCodecInfo jpegCodec = null;
                ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                String mimeType = "image/jpeg";

                int j;
                for (j = 0; j < encoders.Length; ++j) 
                {
                    if (encoders[j].MimeType == mimeType) 
                    jpegCodec = encoders[j]; 
                }
                System.IO.MemoryStream stream = new System.IO.MemoryStream();

                if (f_upload.PostedFile.ContentLength > 153600)
                {
                    EncoderParameters eParam = new EncoderParameters();
                    eParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)40);
                    inputImage.Save(stream, jpegCodec, eParam);
                }
                else
                {
                    inputImage.Save(stream, ImageFormat.Jpeg);
                }
                stream.Position = 0;

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                OleDbCommand insertSqlComm = default(OleDbCommand);
                OleDbCommand updateSqlComm = default(OleDbCommand);

                int img_name;

                insertSqlComm = new OleDbCommand("INSERT INTO post_img(img_path,pid) VALUES('img_path','" + Session["pid"] + "');SELECT SCOPE_IDENTITY() AS 'identity'", conn);
                img_name = Convert.ToInt32(insertSqlComm.ExecuteScalar().ToString());

                string img_path = "./post/img/" + img_name + extension;
                Session["img_path"] = img_path;
                Session["cover_img_id"] = img_name;

                updateSqlComm = new OleDbCommand("update post_img set img_path = '" + @img_path + "' where img_id = '" + img_name + "'", conn);
                updateSqlComm.ExecuteNonQuery();

                conn.Close();

                string strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name + extension;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;
                request.Credentials = new NetworkCredential("bearty", "thFGmAKc");

                string filename = f_upload.PostedFile.FileName.ToString();

                using (Stream s = request.GetRequestStream())
                {
                    byte[] buffer = new byte[16 * 1024];
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        s.Write(buffer, 0, bytesRead);
                    }
                    s.Flush();
                }
                img_cover.ImageUrl = Session["img_path"].ToString();
                img_cover.OnClientClick = "window.open('" + Session["img_path"].ToString() + "','','')";
                img_cover.Visible = true;
                upload_status.Text = filename + " is uploaded.";
                Session["cover_image"] += "<A rel='lightbox' HREF='" + Session["img_path"] + "'><img src='" + Session["img_path"] + "' style='padding:12px;width:100px;height:auto;'/></A>";

                if (upload_btn.Text.ToString().Equals("upload"))
                {
                    upload_btn.Text = "change";

                }

            }
            else
            {
                img_cover.Visible = false;
                if (!extensionFlag)
                {
                    upload_status.Text = "Invalid file type";
                }else if(!filesizeFlag){
                    upload_status.Text = "File size exceeds 2MB";
                }
            }
        }
        else
        {
            img_cover.Visible = false;
            upload_status.Text = "Select a file";
        }
    }

    protected void upload_other_btn_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        FileUpload other_upload_temp = null;
        Label upload_other_status_temp = null;
        Button upload_other_btn_temp = null;
        string indicator = null;
        ImageButton upload_other_img_btn = null;
        if (btn.CommandName == "upload_other")
        {
            other_upload_temp = (FileUpload)FindControl("other_upload" + btn.CommandArgument.ToString());
            upload_other_status_temp = (Label)FindControl("upload_other_status" + btn.CommandArgument.ToString());
            upload_other_btn_temp = (Button)FindControl("upload_other_btn" + btn.CommandArgument.ToString());
            upload_other_img_btn = (ImageButton)FindControl("ImageButton" + btn.CommandArgument.ToString());
            indicator = btn.CommandArgument.ToString();

        }

        if (!other_upload_temp.PostedFile.FileName.ToString().Equals(""))
        {
            if (upload_other_btn_temp.ToString().Equals("change"))
            {
                OleDbConnection chg_conn = new OleDbConnection();
                chg_conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                chg_conn.Open();
                OleDbCommand chg_deleteSqlComm = default(OleDbCommand);

                Session[indicator + "_html_img"] = "";
                string strUri = "ftp://ftp.bearty.hk/web/post/img/" + Session[indicator + "_img"].ToString();

                FtpWebRequest chg_request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                chg_request.Method = WebRequestMethods.Ftp.DeleteFile;
                chg_request.Timeout = (60000 * 1);
                chg_request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                FtpWebResponse chg_response = (FtpWebResponse)chg_request.GetResponse();


                chg_deleteSqlComm = new OleDbCommand("delete from post_img where img_id =" + Session[indicator + "_id"] + "", chg_conn);
                chg_deleteSqlComm.ExecuteNonQuery();
                chg_conn.Close();
            }

            string extension = Path.GetExtension(other_upload_temp.PostedFile.FileName);
            string chkExtension = extension.ToLower();
            Boolean extensionFlag = false;
            Boolean filesizeFlag = true;

            if (chkExtension.Equals(".jpg") || chkExtension.Equals(".jpeg") || chkExtension.Equals(".png"))
                extensionFlag = true;

            if (other_upload_temp.PostedFile.ContentLength > 4194305)
                filesizeFlag = false;

            if (extensionFlag && filesizeFlag)
            {
                int fileLength = Convert.ToInt32(other_upload_temp.PostedFile.InputStream.Length);
                byte[] imageBuffer = new byte[fileLength];
                other_upload_temp.PostedFile.InputStream.Read(imageBuffer, 0, fileLength);
                MemoryStream imageMS = new MemoryStream(imageBuffer);

                System.Drawing.Image inputImage = System.Drawing.Image.FromStream(imageMS);

                ImageCodecInfo jpegCodec = null;
                ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
                String mimeType = "image/jpeg";

                int j;
                for (j = 0; j < encoders.Length; ++j)
                {
                    if (encoders[j].MimeType == mimeType)
                        jpegCodec = encoders[j];
                }

                System.IO.MemoryStream stream = new System.IO.MemoryStream();

                if (other_upload_temp.PostedFile.ContentLength > 153600)
                {
                    EncoderParameters eParam = new EncoderParameters();
                    eParam.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)70);
                    inputImage.Save(stream, jpegCodec, eParam);
                }
                else
                {
                    inputImage.Save(stream, ImageFormat.Jpeg);
                }

                stream.Position = 0;

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                OleDbCommand insertSqlComm = default(OleDbCommand);
                OleDbCommand updateSqlComm = default(OleDbCommand);
                int img_name;

                insertSqlComm = new OleDbCommand("INSERT INTO post_img(img_path,pid) VALUES('img_path','" + Session["pid"] + "');SELECT SCOPE_IDENTITY() AS 'identity'", conn);
                img_name = Convert.ToInt32(insertSqlComm.ExecuteScalar().ToString());

                string img_path = "./post/img/" + img_name + extension;
                Session[indicator + "_img"] = img_name + extension;
                Session[indicator + "_id"] = img_name;

                updateSqlComm = new OleDbCommand("update post_img set img_path = '" + @img_path + "' where img_id = '" + img_name + "'", conn);
                updateSqlComm.ExecuteNonQuery();

                conn.Close();

                string strUri = "ftp://ftp.bearty.hk/web/post/img/" + img_name + extension;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;
                request.Credentials = new NetworkCredential("bearty", "thFGmAKc");

                string filename = other_upload_temp.PostedFile.FileName.ToString();

                using (Stream s = request.GetRequestStream())
                {
                    byte[] buffer = new byte[16 * 1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        s.Write(buffer, 0, bytesRead);
                    }
                    s.Flush();
                }

                upload_other_img_btn.ImageUrl = img_path;
                upload_other_img_btn.OnClientClick = "window.open('" + img_path + "','','')";
                upload_other_img_btn.Visible = true;
                upload_other_status_temp.Text = filename + " is uploaded.";
                

                Session[indicator + "_html_img"] += "<A rel='lightbox' HREF='" + img_path + "'><img src='" + img_path + "' style='padding:12px;width:100px;height:auto;'/></A>";


                if (upload_other_btn_temp.Text.ToString().Equals("upload"))
                {
                    upload_other_btn_temp.Text = "change";

                }
            }
            else
            {
                upload_other_img_btn.Visible = false;
                if (!extensionFlag)
                {
                    upload_other_status_temp.Text = "Invalid file type";
                }
                else if (!filesizeFlag) {
                    upload_other_status_temp.Text = "File size exceeds 2MB";
                }
            }
        }
        else
        {
            upload_other_img_btn.Visible = false;
            upload_other_status_temp.Text = "Select a file";
        }
    }

    protected void event_chkbox_OnSelectedIndexChanged(object sender, EventArgs e) 
    {
        
        ListItem li = event_chkbox.Items[0];

        if (li.Selected)
        {

            etb.Visible = true;
            elb.Visible = true;
            eventDate_lb.Visible = true;
            eventDate_tb.Visible = true;

        }
        else 
        {
            etb.Visible = false;
            elb.Visible = false;
            eventDate_lb.Visible = false;
            eventDate_tb.Visible = false;       
        }

    }

    protected ArrayList GetStringInBetween(string strBegin, string strEnd, string strSource)
    {
        ArrayList result = new ArrayList();
        Boolean flag = true;
        int iIndexOfBegin ,iEnd;
        int beginLength = strBegin.Length;
        int endLength = strEnd.Length;
        do
        {
            iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                strSource = strSource.Substring(iIndexOfBegin + beginLength);

                iEnd = strSource.IndexOf(strEnd); 

                if (iEnd != -1)
                {
                    result.Add(strSource.Substring(0, iEnd));
                    iEnd += endLength;

                    strSource = strSource.Substring(iEnd);
                }
                else
                    flag = false;
            }
            else
                flag = false;
        } while (flag);
        return result;
    }

    protected void share_btn_OnClick(object sender, EventArgs e)
    {

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            conn.Open();

            StringBuilder p_type;
            p_type = new StringBuilder("");
            p_type.Append("dialogue;");
            if (cat_CheckBox.Items[0].Selected)
                p_type.Append("architecture;");
            if (cat_CheckBox.Items[1].Selected)
                p_type.Append("art;");
            if (cat_CheckBox.Items[2].Selected)
                p_type.Append("design;");
            if (cat_CheckBox.Items[3].Selected)
                p_type.Append("film;");
            if (cat_CheckBox.Items[4].Selected)
                p_type.Append("photography;");
            if (cat_CheckBox.Items[5].Selected)
                p_type.Append("institutions;");
            if (cat_CheckBox.Items[6].Selected)
                p_type.Append("travel;");
            if (event_chkbox.Items[0].Selected)
                p_type.Append("event;");
            if (own_chkbox.Items[0].Selected)
                p_type.Append("original;");

            if (event_chkbox.Items[0].Selected && eventDate_tb.Text.ToString().Trim().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('Choose a date to hold the event.');</script>");
            }else if (p_type.ToString().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('Choose at least one category.');</script>");
            }
            else if (title.Text.ToString().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>alert('Give a title.');</script>");
            }
            else
            {

                    string filePath = "./post/content/" + Session["pid"] + ".txt";

                    string integrat_post = "";
					if(Session["img_path"] != null && !TextAreaEditor.Text.ToString().Trim().Equals(""))
						integrat_post = "</br>" + TextAreaEditor.Text.ToString().Replace(Environment.NewLine, "<br/>") + "<hr></br>";

                    TextBox temp_subtitle = null;
                    TextBox temp_desc = null;
                    
                    for (int i = 1; i < 10; i++)
                    {
                        temp_subtitle = (TextBox)FindControl("subtitle" + i);
                        temp_desc = (TextBox)FindControl("TextAreaEditor" + i);

                        if (Session[i + "_img"] != null)
                        {
                            integrat_post += "<A rel='lightbox' HREF='./post/img/" + Session[i + "_img"].ToString() + "'><div style='width:753px;text-align:center;'><img src='./post/img/" + Session[i + "_img"].ToString() + "' style='max-width: 753px;min-width: 500px;vertical-align:middle;margin-top:15px;'/></div></A>";
                        }

                        if (!temp_subtitle.Text.ToString().Trim().Equals(""))
                        {
                            integrat_post += "<div><b><font style='font-size:11px;-webkit-text-size-adjust:none;'>" + temp_subtitle.Text.ToString() + "</font></b></br></div></br>";
                        }

                        if (!temp_desc.Text.ToString().Trim().Equals(""))
                        {
                            integrat_post += temp_desc.Text.ToString() + "</br></br>";
                        }

                    }

                    if (!yt_title.Text.ToString().Trim().Equals(""))
                        integrat_post += "<b><font style='font-size:11px;-webkit-text-size-adjust:none;'>" + yt_title.Text.ToString() + "</font></b>";

                    if (!yt_content.Text.ToString().Trim().Equals(""))
                    {
                        yt_content.Text = yt_content.Text.ToString().Replace("[youtube]", "</br><div style='text-align: center;'><iframe width='753' height='458' src='http://www.youtube.com/embed/");
                        yt_content.Text = yt_content.Text.ToString().Replace("[/youtube]", "' frameborder='0' allowfullscreen></iframe></div></br></br>");

                        integrat_post += yt_content.Text.ToString() + "</br>";
                    }

					if(Session["cover_Image"] != null)
						integrat_post += "</br></br>" + Session["cover_Image"].ToString();
                    for (int i = 1; i < 10; i++)
                    {
                        if (Session[i + "_html_img"] != null)
                            integrat_post += Session[i + "_html_img"].ToString();
                    }


                    OleDbCommand selectSqlComm = default(OleDbCommand);
                    string temppid = Session["pid"].ToString();
                    selectSqlComm = new OleDbCommand("select count(*) as CRT from ba_post where pid='" + temppid + "';", conn);
                    int chkLoss = Convert.ToInt32(selectSqlComm.ExecuteScalar().ToString());
                    OleDbCommand updteSqlComm = default(OleDbCommand);

                    if (chkLoss > 0)
                    {
                        string strUri = "ftp://ftp.bearty.hk/web/post/content/" + Session["pid"] + ".txt";
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(strUri));
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.KeepAlive = false;
                        request.UseBinary = true;
                        request.UsePassive = true;
                        request.Credentials = new NetworkCredential("bearty", "thFGmAKc");
                        using (Stream s = request.GetRequestStream())
                        {
                            byte[] buffer = new byte[16 * 1024];
                            int bytesRead;
                            byte[] byteArray = Encoding.UTF8.GetBytes(integrat_post);
                            MemoryStream i_stream = new MemoryStream(byteArray);
                            while ((bytesRead = i_stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                s.Write(buffer, 0, bytesRead);
                            }
                            s.Flush();
                        }


                        string ctitle = title.Text;
                        ctitle = ctitle.Replace("'", "&#39;");
                        ctitle = ctitle.Replace("\"","&#34;");
                        //ctitle = ctitle.Replace(";", "&#59;");
                        ctitle = ctitle.Replace("(", "&#40;");
                        ctitle = ctitle.Replace(")", "&#41;");
                        ctitle = ctitle.Replace("+", "&#43;");

						string imgpath;
						if(Session["img_path"] != null)
							imgpath = Session["img_path"].ToString();
						else
							imgpath = "./img/line.png";
                        if (event_chkbox.Items[0].Selected)
                        {
                            updteSqlComm = new OleDbCommand("update ba_post set event_date = '" + Session["eDate"].ToString().Trim() + " 01:00:00', time = '" + DateTime.Now.ToString("yyyy/MMM/dd HH:mm:ss") + "', p_content = '" + @filePath + "',title = N'" + ctitle + "',status = 'approved',p_type='" + p_type.ToString() + "',p_img='" + imgpath + "' where pid = '" + Session["pid"] + "';", conn);
                        }
                        else
                        {
                            updteSqlComm = new OleDbCommand("update ba_post set time = '" + DateTime.Now.ToString("yyyy/MMM/dd HH:mm:ss") + "', p_content = '" + @filePath + "',title = N'" + ctitle + "',status = 'approved',p_type='" + p_type.ToString() + "',p_img='" + imgpath + "' where pid = '" + Session["pid"] + "';", conn);

                        } 
                        int rowsAffected;
                        rowsAffected = updteSqlComm.ExecuteNonQuery();

                        if (!retb.Text.ToString().Trim().Equals(""))
                        {
                            updteSqlComm = new OleDbCommand("update ba_post set related_post = N'" + retb.Text.ToString() + "' where pid ='" + Session["pid"] + "';", conn);
                            updteSqlComm.ExecuteNonQuery();

                            String restr = retb.Text.ToString();
                            try
                            {
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

                                        selectSqlComm = new OleDbCommand("select related_post from ba_post where pid = '" + restr + "'", conn);
                                        String related_post = "";
                                        if(selectSqlComm.ExecuteScalar()!=null)
                                            related_post = selectSqlComm.ExecuteScalar().ToString();
                                        related_post += ";http://bearty.hk/postdetail.aspx?pid=" + Session["pid"];

                                        updteSqlComm = new OleDbCommand("update ba_post set related_post = N'" + related_post + "' where pid ='" + restr + "';", conn);
                                        updteSqlComm.ExecuteNonQuery();
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
                                                    selectSqlComm = new OleDbCommand("select related_post from ba_post where pid = '" + oleReader2["pid"].ToString() + "'", conn);
                                                    String related_post2 = "";
                                                    
                                                    if(selectSqlComm.ExecuteScalar()!=null)
                                                        related_post2 = selectSqlComm.ExecuteScalar().ToString();

                                                    related_post2 += ";http://bearty.hk/postdetail.aspx?pid=" + Session["pid"];

                                                    updteSqlComm = new OleDbCommand("update ba_post set related_post = N'" + related_post2 + "' where pid ='" + oleReader2["pid"].ToString() + "';", conn);
                                                    updteSqlComm.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        oleReader2.Close();
                                    }

                            }
                            catch (OleDbException exx) { }
                            
                            }

                        conn.Close();

                        Session["Image"] = "";
                        Session["img_path"] = null;
                        Session["cover_counter"] = 0;
                        Session["pid"] = "";
                        Session["eDate"] = null;
                        Session["cover_Image"] = null;
                        for (int i = 1; i < 10; i++)
                        {
                            Session[i + "_id"] = null;
                            Session[i + "_img"] = null;
                            Session[i + "_html_img"] = null;
                        }

                        Response.Redirect("submit_post.aspx");
                    }
                    else
                    {
                        Response.Write("<Script language='JavaScript'>alert('System maintanence. Sorry for inconvenience.');</Script>");
                    }


            }

    }


    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex is HttpRequestValidationException)
        {
            Response.Write("<Script language='JavaScript'>alert('HTML tags are unavailable in description.');</Script>");
            Server.ClearError();
            Response.AppendHeader("REFRESH", "0.1;URL='http://bearty.hk/share.aspx'");

        }
    }
}

