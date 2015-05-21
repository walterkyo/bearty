using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Calendar : System.Web.UI.Page
{
    public StringBuilder strDate = new StringBuilder("");

    protected void Page_Load(object sender, EventArgs e)
    {
        Calendar2.SelectionChanged += new EventHandler(CalendarOnSelectionChanged);
    }

    protected void CalendarOnSelectionChanged(object sender, EventArgs e)
    {
        Session["eDate"] += Calendar2.SelectedDate.Date.ToString("yyyy-MMM-dd") + " ";
        string sTextBoxID = Request.QueryString["TextBoxId"];
        strDate.Append(Calendar2.SelectedDate.ToString("yyyy-MMM-dd"));
        string sScript = "if(opener.window.document.getElementById('" + sTextBoxID + "').value == 'Click here to select a date.') {opener.window.document.getElementById('" + sTextBoxID + "').value = '';}opener.window.document.getElementById('" + sTextBoxID + "').value+='" + strDate.ToString() + " ';";
        ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>" + sScript + "</script>"); 
     }

    protected void btnOK_OnClick(object sender, EventArgs e)
    {
        string sScript;
        sScript = "window.close();";
        ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>" + sScript + "</script>"); 
        /*
        string sScript; 
        string sTextBoxID;
        
        sTextBoxID = Request.QueryString["TextBoxId"];
        if (Calendar2.SelectedDate.Date.ToString("yyyy/MMM/dd").Equals("0001/Jan/01"))
        {
            Session["eDate"] += DateTime.Today.ToString("yyyy-MMM-dd") + " ";
            sScript = "opener.window.document.getElementById('" + sTextBoxID + "').value='" + DateTime.Today.ToString("yyyy-MMM-dd") + " ';";
        }
        else
        {
            Session["eDate"] += Calendar2.SelectedDate.Date.ToString("yyyy-MMM-dd") + " ";
            //sScript = "if(opener.window.document.getElementById('" + sTextBoxID + "').value == 'Click here to select a date.') {opener.window.document.getElementById('" + sTextBoxID + "').value = '';}opener.window.document.getElementById('" + sTextBoxID + "').value+='" + Calendar2.SelectedDate.Date.ToString("yyyy-MMM-dd") + " ';";
            sScript = "if(opener.window.document.getElementById('" + sTextBoxID + "').value == 'Click here to select a date.') {opener.window.document.getElementById('" + sTextBoxID + "').value = '';}opener.window.document.getElementById('" + sTextBoxID + "').value+='" + strDate.ToString() + " ';";
        } sScript = sScript + "window.close();";
        ClientScript.RegisterStartupScript(GetType(), "JsScript", "<script type='text/javascript'>" + sScript + "</script>"); 
         */
    }
}