﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopdetail.aspx.cs" Inherits="shopdetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>beARTY</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Scripts/url2link.js"></script>
    <link rel="shortcut icon" href="img/favicon.ico" />
</head>
<script runat="server">
    string strPageNo = "Page ";
</script>
<div id="fb-root"></div>
<script src='http://connect.facebook.net/en_US/all.js'></script>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=439354912782535";
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
            <asp:Textbox runat="server" ID="search_bar" CssClass="search_bar" style="background:transparent;position:absolute;top:47%;left:26.7%;"/>
            <asp:ImageButton ID="search_img_btn" ImageUrl="./img/search_btn.gif" OnClick="search_btn_OnClick" runat="server" style="position:absolute;top:51%;left:26.7%;" onmouseover="this.src='./img/search_btn_hover.gif'" onmouseout="this.src='./img/search_btn.gif'"/>
            </span>

            <span class="menu">
                <a href="about.aspx" style="position:absolute;left:97%;top:20%;">中文</a></br>
                
                <asp:LinkButton ID="login_lnk" runat="server" visible="true" PostBackUrl="~/login.aspx" style="position:absolute;left:77.2%;top:43%;">login</asp:LinkButton>
                <asp:LinkButton ID="logout_lnk" runat="server" visible="false" OnClick="logout_lnk_OnClick" style="position:absolute;left:77.2%;top:43%;">logout</asp:LinkButton>
                <a href="about.aspx" style="position:absolute;left:89.5%;top:43%;">advertise with us</a>
                </br>
                <a href="about.aspx" style="position:absolute;left:77.1%;top:58%;">about beARTY</a>
                <a href="about.aspx" style="position:absolute;left:93.5%;top:58%;">contact us</a>
                

            </span>

            </div>

                
          
    <div style="position:absolute;left:69%;-webkit-text-size-adjust:none;font-size:13px;"><asp:HyperLink ID="username" Font-Underline="true" ForeColor="Black" runat="server" Visible="false"></asp:HyperLink></div>  

            </div>
        </div>

    </br></br>
    <div class="wrapper">
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
        
            <div class="post">
            <div class="content">
            <asp:Literal ID="pro_detail" runat="server"></asp:Literal>
            </div>
            </br>
            <div style="float:right;"></div>
            

            </div>
            
        <br />
        <br />

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM [ba_product] ORDER BY [time] DESC">
        </asp:SqlDataSource>



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
        </div>
        </div>
<div class="wrapper">
<div class="footer">© copyrights 2000 - 2012 beARTY, all rights reserved. all material published remains the exclusive copyright of beARTY.  <br/>

</div>
</div>
    </form>

<script>
    updateURL2Link();
</script>
</body>
</html>
