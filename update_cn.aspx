<%@ Page Language="C#" Culture="en-US" AutoEventWireup="true" CodeFile="update_cn.aspx.cs" Inherits="update_cn" %>
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
                <a href="update.aspx" style="position:absolute;left:77%;top:83%;font-family:Adobe 繁黑體 Std B;">English</a></br>

                <asp:LinkButton ID="logout_lnk" runat="server" visible="false" OnClick="logout_lnk_OnClick" style="position:absolute;left:97.3%;top:64%;font-family:Adobe 繁黑體 Std B;">登出</asp:LinkButton>
                <a href="advertise_cn.aspx" style="position:absolute;left:97.3%;top:83%;font-family:Adobe 繁黑體 Std B;">廣告</a>
                </br>
                <a href="about_cn.aspx" style="position:absolute;left:77.1%;top:102%;font-family:Adobe 繁黑體 Std B;">關於 beARTY</a>
                <a href="contact_cn.aspx" style="position:absolute;left:95%;top:102%;font-family:Adobe 繁黑體 Std B;">聯絡我們</a>
                

            </span>


            </div>
        </div>

    
</br></br>

<div class="wrapper" style="margin-top:10px;">
</br></br>
	<div class="pagebody">
		<div class="body">
        <div class="catbar">
                    <asp:Button ID="architecture" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="建築"/>
                    <asp:Button ID="art" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="藝術"/>
                    <asp:Button ID="design" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="設計"/>
                    <asp:Button ID="film" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="電影"/>
                    <asp:Button ID="photography" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="攝影"/>
                    <asp:Button ID="institutions" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="學院"/>
                    <asp:Button ID="travel" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="旅遊"/>
                    <asp:Button ID="events" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="活動"/>
                </div>

    	<div class="insertform">
    
       <table width="100%">
       <tr><td width="25%">舊密碼</td>
        <td width="25%"><asp:TextBox ID="opwd" runat="server" TextMode="password"></asp:TextBox></td><td width="50%">&nbsp;</td>
		</tr>
        <tr><td>別名</td>
        <td><asp:TextBox ID="nickname" runat="server" ></asp:TextBox></td><td></td></tr>
        <tr><td>電郵</td><td><asp:TextBox ID="email" runat="server" ></asp:TextBox></td><td><asp:RegularExpressionValidator ID="emailform" runat="server"     
                                    ErrorMessage="This email does not validate." 
                                    ControlToValidate="email"     
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"/></td></tr>
        <tr><td>新密碼</td><td><asp:TextBox ID="pwd" runat="server" TextMode="password"></asp:TextBox></td><td><asp:Label ID="onn" runat="server"/></td></tr>
        <tr><td>Confirm New Password</td><td><asp:TextBox ID="cpwd" runat="server" TextMode="Password"></asp:TextBox></td><td><asp:CompareValidator 
        	  id="comparePasswords" 
              runat="server"
              ControlToCompare="pwd"
              ControlToValidate="cpwd"
              ErrorMessage="Your passwords do not match up!"
              Display="Dynamic" /></td></tr>
        
        <tr><td>頭像</td><td> <asp:FileUpload id="p_upload" type="file" width = "360px" runat="server" /></td><td><asp:CustomValidator ID="CustomValidator1" runat="server" 
 ClientValidationFunction="ValidateFileUpload" ErrorMessage="Please select valid image file" Display="Dynamic"></asp:CustomValidator></td></tr>
        <tr><td colspan="3"><asp:Button ID="Submit_Btn" onClick="Submit_Btn_OnClick" CssClass="content_btn_cn" runat="server" Text="提交"/></td></tr>
        
        
    	</table>
    </div>

        </div>
		<div class="siderbar">
             <div style="margin-top:-9px;width:260px;">
                <asp:Button ID="share_btn" runat="server" CssClass ="content_btn" OnClick="share_btn_OnClick" Text="分享" style="margin-left:0%;"/>
                <asp:Button ID="login_btn" runat="server" CssClass ="content_btn" OnClick="login_btn_OnClick" Text="登入" style="float:right;"/>
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
            <font color="Red" class="blackfont">最新發表</font>
            </div>
            <div class="recent_post">
            <asp:ListView ID="RecentPostListView" runat="server" DataKeyNames="pid" 
            DataSourceID="SqlDataSource2" >
            <EmptyDataTemplate>
                <span>No posts right now.</span>
            </EmptyDataTemplate>
            
            <ItemTemplate>
                <div>
                <a href="postdetail_cn.aspx?pid=<%#Eval("pid") %>" class="rp_link" title="<%#Eval("title") %>">
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
            <font color="Red" class="blackfont">最後留言</font>
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
                <a href="postdetail_cn.aspx?pid=<%#Eval("pid") %>#comments" class="rp_link" title="<%#Eval("title") %>">
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
            <a alt="FAO Shop - Fashion Art Object" title="FAO Shop - Fashion Art Object" style="text-decoration: none;height:50px;margin-bottom:10px;margin-right:5px;float:left;" href="http://www.fao-shop.com/" target="_blank">
                        <img src="./img/fao-logo.gif" style="height:100px;"/>
            </a>
            <a alt="Voxfire Gallery" title="Voxfire Gallery" style="text-decoration: none;margin-bottom:10px;float:right;height:50px;" href="http://www.voxfiregallery.com/home.php" target="_blank">
                        <img src="./img/Voxfire_Gallery.png" style="height:100px;"/>
            </a>
            <br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
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
