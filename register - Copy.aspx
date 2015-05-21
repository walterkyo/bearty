﻿<%@ Page Language="C#" Culture="en-US" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %> 
    <title>beARTY</title>
	<link href="style.css" rel="stylesheet" type="text/css" />
	<link rel="shortcut icon" href="img/favicon.ico" />
    <script language="javascript" type="text/javascript">
		function ValidateFileUpload(Source, args)
		{
		  var fuData = document.getElementById('<%= p_upload.ClientID %>'); 
		  var FileUploadPath = fuData.value;
		 
		  if(FileUploadPath =='') 
		  {
			  args.IsValid = true;
		  }
		  else
		  {
			var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
		 
			if (Extension == "png" || Extension == "jpg" || Extension == "jpeg" || Extension == "gif")
			{
			  args.IsValid = true; // Valid file type
			}
			else
			{
			  args.IsValid = false; // Not valid file type
			}
		   }
		}
	</script> 
</head>
<div id="fb-root"></div>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));
</script>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
	    <div class="header">
            <div>
            <A href="http://bearty.hk"><img src="img/logo.png"width="266" height="64" style="margin-left:20px;"/></A>
            <span>
            <asp:Textbox runat="server" ID="search_bar" CssClass="search_bar" style="background:transparent;position:absolute;top:28%;left:26.7%;"/>
            <asp:ImageButton ID="search_img_btn" ImageUrl="./img/search_btn.gif" OnClick="search_btn_OnClick" runat="server" style="position:absolute;top:30%;left:26.7%;" onmouseover="this.src='./img/search_btn_hover.gif'" onmouseout="this.src='./img/search_btn.gif'"/>
            </span>
            </div>

            </br></br>

            <div style="width:850px;">
                    <a alt="decor collection 帝家設計" title="decor collection 帝家設計" style="text-decoration: none;position:absolute;bottom:-20%;left:16%;" href="http://www.decorcollection.com" target="_blank">
                        <img src="./img/super_header.gif"/>
                    </a>

            <span class="menu">
                <a href="register_cn.aspx" style="position:absolute;left:97%;top:68%;">中文</a></br>
                
                <asp:LinkButton ID="login_lnk" runat="server" visible="true" PostBackUrl="~/login.aspx" style="position:absolute;left:77.2%;top:83%;">login</asp:LinkButton>
                <asp:LinkButton ID="logout_lnk" runat="server" visible="false" OnClick="logout_lnk_OnClick" style="position:absolute;left:77.2%;top:83%;">logout</asp:LinkButton>
                <a href="advertise.aspx" style="position:absolute;left:89.7%;top:83%;">advertise with us</a>
                </br>
                <a href="about.aspx" style="position:absolute;left:77.1%;top:98%;">about beARTY</a>
                <a href="contact.aspx" style="position:absolute;left:93.5%;top:98%;">contact us</a>
                

            </span>

            </div>

            <div style="position:absolute;top:68%;left:77.2%;-webkit-text-size-adjust:none;font-size:13px;"><asp:HyperLink ID="username" Font-Underline="true" ForeColor="Black" runat="server" Visible="false"></asp:HyperLink> <asp:HyperLink ID="update" Font-Underline="true" ForeColor="Black" runat="server" Visible="false"></asp:HyperLink></div>
            

            </div>
        </div>
</br></br>

<div class="wrapper" style="margin-top:10px;">
</br></br>
	<div class="pagebody">
		<div class="body">
        <div class="catbar">
                    <asp:Button ID="architecture" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="architecture"/>
                    <asp:Button ID="art" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="art"/>
                    <asp:Button ID="design" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="design"/>
                    <asp:Button ID="film" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="film"/>
                    <asp:Button ID="photography" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="photography"/>
                    <asp:Button ID="institutions" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="institutions"/>
                    <asp:Button ID="travel" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="travel"/>
                    <asp:Button ID="events" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="event"/>
                </div>

    	<div class="insertform">
    	<asp:UpdatePanel ID="PnlUsrDetails" runat="server">
        <ContentTemplate>
        <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
       <table width="100%">
       <tr><td width="25%"><font style="font-family:Arial;-webkit-text-size-adjust:none;font-size:13px;">Login Name</font></td>
        <td width="25%"><asp:TextBox ID="login_name" runat="server" AutoPostBack="true" ontextchanged="txtUsername_TextChanged"></asp:TextBox></td><td width="50%"><div id="checkusername" runat="server"  Visible="false" style="color:red"><asp:Label ID="lblStatus" runat="server"></asp:Label></div><asp:RequiredFieldValidator runat="server" style="font-family:Arial;-webkit-text-size-adjust:none;font-size:13px;" id="reqName" controltovalidate="login_name" errormessage="Enter your login name" ValidationGroup="registration"/></td>
		</tr>
        <tr><td>Nickname</td>
        <td><asp:TextBox ID="nickname" runat="server" ></asp:TextBox></td><td></td></tr>
        <tr></tr>
        <tr><td>Password</td><td><asp:TextBox ID="pwd" runat="server" TextMode="password"></asp:TextBox></td><td><asp:RequiredFieldValidator runat="server" id="reqPw" controltovalidate="pwd" errormessage=""  ValidationGroup="registration"/></td></tr>
        <tr><td>Confirm Password</td><td><asp:TextBox ID="cpwd" runat="server" TextMode="Password"></asp:TextBox></td><td><asp:CompareValidator 
        	  id="comparePasswords" 
              runat="server"
              ControlToCompare="pwd"
              ControlToValidate="cpwd"
              ErrorMessage="Your passwords do not match up!"
              Display="Dynamic" ValidationGroup="registration" /></td></tr>
        <tr><td>Email</td><td><asp:TextBox ID="email" runat="server" AutoPostBack="true" ontextchanged="email_TextChanged"></asp:TextBox></td><td><asp:RequiredFieldValidator runat="server" id="reqEmail" controltovalidate="email" errormessage="" /><asp:RegularExpressionValidator ID="emailform" runat="server"     
                                    ErrorMessage="This email does not validate." 
                                    ControlToValidate="email"     
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ValidationGroup="registration"/><div id="checkemail" runat="server"  Visible="false" style="color:red"><asp:Label runat="server" ID="emailStatus" /></div>
</td></tr>
        <tr><td>Profile Picture</td><td> <asp:FileUpload id="p_upload" type="file" width = "360px" runat="server" /></td><td><asp:CustomValidator ID="CustomValidator1" runat="server" 
 ClientValidationFunction="ValidateFileUpload" ErrorMessage="Please select valid image file" Display="Dynamic"></asp:CustomValidator></td></tr>
 		<tr><td colspan="2"><cc1:CaptchaControl ID="ccJoin" runat ="server" CaptchaBackgroundNoise="none" CaptchaLength="5" CaptchaHeight ="60" CaptchaWidth="200" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240" /> 
</td><td><asp:Label runat="server" ID="errMsg"/></td></tr>
		<tr><td colspan="3">Type in the text above:<asp:TextBox ID="txtCap" runat="server" Width="200"/></td></tr>
        <tr><td colspan="3"><asp:Button ID="Submit_Btn" onClick="Submit_Btn_OnClick" CssClass="content_btn" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="registration"/></td></tr>
        
        
    	</table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>

        </div>
		<div class="siderbar">
             <div style="margin-top:-9px;width:260px;">
                <asp:Button ID="shop_btn" runat="server" CssClass ="content_btn" OnClick="shop_btn_OnClick" Text="Shop" style="margin-left:0%;"/>
                <asp:Button ID="share_btn" runat="server" CssClass ="content_btn" OnClick="share_btn_OnClick" Text="Share" style="float:right;"/>
            </div>
            </br>

            <div>


            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                BorderColor="#9C9C9C" DayNameFormat="FirstLetter" Font-Names="Times New Roman" 
                Font-Size="10pt" ForeColor="#FF0000" Height="205px" NextPrevFormat="CustomText"
                TitleFormat="MonthYear" Width="250px" BorderStyle="None" BorderWidth="4px" 
                NextMonthText="»" PrevMonthText="«" OnDayRender="Calendar1_OnDayRender" >
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" 
                    ForeColor="#333333" Height="10pt" />
                <DayStyle Width="14%" />
                <NextPrevStyle Font-Size="12pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="8pt" ForeColor="#333333" Width="1%" />
                <TitleStyle BackColor="#9C9C9C" Font-Bold="True" Font-Size="13pt" 
                    ForeColor="Black" Height="13pt" />
                <TodayDayStyle BackColor="#E6E6FA" />

            </asp:Calendar>

            </br>
            <div style="padding:10px;">
            <font color="Red">Recent posts</font>
            </div>
            <div class="recent_post">
            <asp:ListView ID="RecentPostListView" runat="server" DataKeyNames="pid" 
            DataSourceID="SqlDataSource2" >
            <EmptyDataTemplate>
                <span>No posts right now.</span>
            </EmptyDataTemplate>
            
            <ItemTemplate>
                <div>
                <a href="postdetail.aspx?pid=<%#Eval("pid") %>" class="rp_link" title="<%#Eval("title") %>">
                <%# DataBinder.Eval(Container.DataItem,"title").ToString().Length > 30 ?
                string.Concat(DataBinder.Eval(Container.DataItem, "title").ToString().Substring(0,30),"…") :
                DataBinder.Eval(Container.DataItem,"title"). ToString().Substring(0,DataBinder.Eval(Container.DataItem,"title").ToString().Length)%>
                </a>
                </div>
            </ItemTemplate>
            <LayoutTemplate>
                <div ID="itemPlaceholderContainer" runat="server" style="">
                    <span runat="server" id="itemPlaceholder" />
                </div>
            </LayoutTemplate>
            
        </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT TOP 10 * FROM [ba_post] where [status] = 'approved' order by [time] desc;"></asp:SqlDataSource>
            </div>
            </br>
            <div style="padding:10px;">
            <font color="Red">Comments</font>
            </div>

            <div class="recent_post">

            <asp:ListView ID="RecentCMListView" runat="server" DataKeyNames="pid" 
            DataSourceID="SqlDataSource3" OnItemDataBound="RecentCMListView_OnItemDataBound">
            <EmptyDataTemplate>
                <span>No comments right now.</span>
            </EmptyDataTemplate>
            
            <ItemTemplate>
                <div >
                <div style="float:right;"><img ID="detailCM_btn" style="min-width:11px;cursor:pointer;" runat="server" src="./img/plus-button.gif"/>
                <img ID="simpleCM_btn" runat="server" style="min-width:11px;display:none;cursor:pointer;" src="./img/minus-button.gif"/>
                </div>
                <a href="postdetail.aspx?pid=<%#Eval("pid") %>#comments" class="rp_link" title="<%#Eval("title") %>">
                <%# DataBinder.Eval(Container.DataItem,"title"). ToString().Length > 25 ?
                string.Concat(DataBinder.Eval(Container.DataItem, "title").ToString().Substring(0,25),"…") :
                DataBinder.Eval(Container.DataItem,"title"). ToString().Substring(0,DataBinder.Eval(Container.DataItem,"title").ToString().Length)%>
                </a>
                </div>
                <div>
                <asp:Label ID="cm_content" Font-Size="Small" runat="server" Mode="Encode" CssClass="cm_detail_div" style="display:none"></asp:Label></div>
            </ItemTemplate>
            <LayoutTemplate>
                <div ID="itemPlaceholderContainer" runat="server" style="">
                    <span runat="server" id="itemPlaceholder" />
                </div>
            </LayoutTemplate>
            
        </asp:ListView>

        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="select TOP 10 * from [post_cm] order by [time] desc;"></asp:SqlDataSource>
            </div>

            </br>
            </br>
            <div class="fb-like-box" data-href="http://www.facebook.com/pages/BeARTY/264189270366250" data-width="250" data-show-faces="true" data-stream="false" data-header="false" ></div>
            </br>
            </br>
            <a alt="FAO Shop - Fashion Art Object" title="FAO Shop - Fashion Art Object" style="text-decoration: none;" href="http://www.fao-shop.com/" target="_blank">
                        <img src="./img/fao-logo.gif" style="height:100px;"/>
            </a>
        </div>
        </div>
	</div>
</div>
<div class="wrapper">
<div class="footer">© copyrights 2000 - 2012 beARTY, all rights reserved. all material published remains the exclusive copyright of beARTY. <br/>

</div>
</div>
</form>
</body>
</html>
