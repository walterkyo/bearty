﻿<%@ Page Language="C#" Culture="en-US" AutoEventWireup="true" CodeFile="postdetail_cn.aspx.cs" Inherits="postdetail_cn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" 
	  xmlns:og="http://ogp.me/ns#"
      xmlns:fb="http://www.facebook.com/2008/fbml">
<head id="Head1" runat="server">
    <meta property="og:type" content="blog"/>
	<meta property="og:site_name" content="beArty"/>
	<meta property="fb:admins" content="646700923"/>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>beARTY</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Scripts/url2link.js"></script>
    <link rel="shortcut icon" href="img/favicon.ico" />
    <script type="text/javascript" src="Scripts/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery.lightbox-0.5.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.lightbox-0.5.css" media="screen" />

</head>
<script runat="server">
    string strPageNo = "Page ";
</script>

<script type="text/javascript">
    $(function () {
        $('a[@rel*=lightbox]').lightBox({ maxWidth: 1024, maxHeight: 800 });

    });
</script>
<body>
<div id="fb-root"></div>
<script src='http://connect.facebook.net/en_US/all.js'></script>
<script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=439354912782535";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>
    <form id="form1" runat="server">
    <div class="wrapper">
	    <div class="header">
            <div>
            <A href="http://bearty.hk/post_cn.aspx"><img src="img/logo.png"width="266" height="64" style="margin-left:20px;"/></A>
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
                <a href="postdetail.aspx" style="position:absolute;left:77%;top:83%;font-family:Adobe 繁黑體 Std B;">English</a></br>

                <asp:LinkButton ID="logout_lnk" runat="server" visible="false" OnClick="logout_lnk_OnClick" style="position:absolute;left:97.3%;top:64%;font-family:Adobe 繁黑體 Std B;">登出</asp:LinkButton>
                <a href="advertise_cn.aspx" style="position:absolute;left:97.3%;top:83%;font-family:Adobe 繁黑體 Std B;">廣告</a>
                </br>
                <a href="about_cn.aspx" style="position:absolute;left:77.1%;top:102%;font-family:Adobe 繁黑體 Std B;">關於 beARTY</a>
                <a href="contact_cn.aspx" style="position:absolute;left:95%;top:102%;font-family:Adobe 繁黑體 Std B;">聯絡我們</a>
                

            </span>

            </div>

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
                    <asp:Button ID="events" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="活動"/>
                </div>
        
            <div class="post">
            <div class="content">
            <asp:Literal ID="p_content" runat="server"></asp:Literal>
            </div>
            </br></br>
                        <div>
                <span style="width:775px;position:relative;">
                    <asp:Button ID="rate" style="margin-bottom:20px;margin-right:15px;margin-left:700px;" runat="server" CssClass="dp_rating" OnClick="rating_OnClick" Text="趣" onClientClick="if(!confirm('Confirm to rate this post.'))return false;"/>
                 </span>
                <div style="position:relative;display:block;height:auto;margin-top:-20px;">
                <div style="position:absolute;left:79%;bottom:0%;overflow:hidden;float:left;" ><a href="javascript:void(window.open('http://www.facebook.com/share.php?u='.concat(encodeURIComponent(location.href)),'sharer','top='+(screen.height-436)/2+',left='+(screen.width-626)/2+', width=626,height=436,toolbar=0,status=0,resizeable=no'));"><img src="./img/facebook.png" border="0"></a></div>
                </div>
                </br>
                <div style="padding-left:5px;">
                <asp:Label ID="relb" runat="server" style="font-size:15px;color:Red;" Visible="false">Related event:</br></asp:Label>
                <asp:Literal ID="relink" runat="server"></asp:Literal>
                </div>

                </div>
                </br>

            </div>
            </br>
            <div style="float:right;"><fb:share-button type="button"></fb:share-button></div>
            <asp:Button ID="del_btn" runat="server" CssClass ="cat_white" OnClick="del_btn_OnClick" Text="Delete post" onClientClick="if(!confirm('Confirm to delete the post.'))return false;"/>

            </div>
            
        <br />
        <br />

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM [ba_post] where [status] = 'approved' order by [time] desc;">
        </asp:SqlDataSource>
        <div style="-webkit-text-size-adjust:none;font-size:14px;">
        <a name="comments"></a>
        <font color="Red">發表回應:</font></div>
        </br>
        <asp:ListView ID="CommentListView" runat="server" DataKeyNames="cid" 
            DataSourceID="SqlDataSource2">
            <EmptyDataTemplate>
                </br>
                </br>
                <span style="-webkit-text-size-adjust:none;font-size:12px;">No comments right now.</span>
                </br></br>
            </EmptyDataTemplate>
            
            <ItemTemplate>
                <div style="padding-bottom:10px;padding-top:5px;height:auto;padding-left:5px;">
                <a href="profile.aspx?user=<%#Eval("author") %>" class="cm_link"><%#Eval("author") %></a></br>
                <div style="-webkit-text-size-adjust:none;font-size:12px;">
                <asp:Label ID="datetime" runat="server" style="color: Gray"><%#Eval("time")%></asp:Label>&emsp;
                <asp:Label ID="ip" runat="server" style="color: Gray"><%# DataBinder.Eval(Container.DataItem, "author_IP").ToString().Split('.')[0] + "." + DataBinder.Eval(Container.DataItem, "author_IP").ToString().Split('.')[1] + ".*.*"%></asp:Label></br></br>
                <asp:Literal ID="p_cm" runat="server" Text='<%#Eval("comment")%>'/>
                </div>
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
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"></asp:SqlDataSource>
        </br>
        <asp:Textbox ID="post_cm" runat="server" TextMode="MultiLine" Height="150px" width="100%"/>
        </br></br>
        <asp:Button ID="cm_btn" runat="server" CssClass="content_btn_cn" Text="張貼留言" OnClick ="cm_btn_OnClick"/></br></br>


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
            DataSourceID="SqlDataSource3" >
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
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
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
            DataSourceID="SqlDataSource4" OnItemDataBound="RecentCMListView_OnItemDataBound">
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

        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
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
<div class="wrapper">
<div class="footer"></br>© beARTY. All rights reserved.</br>All images are © each office/photographer mentioned.

</div>
</div>
    </form>

<script>
    updateURL2Link();
</script>


</body>
</html>
