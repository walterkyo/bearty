<%@ Page Language="C#" Culture="en-US" AutoEventWireup="true" CodeFile="about_cn.aspx.cs" Inherits="about_cn" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>beARTY</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Scripts/url2link.js"></script>
    <link rel="shortcut icon" href="img/favicon.ico" />

    <style type="text/css">
        .style1
        {
            text-align: center;
            font-family: "Adobe 黑体 Std R";
        }
        .style2
        {
            color: #FF0000;
        }
    </style>

</head>
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
                <a href="about.aspx" style="position:absolute;left:77%;top:83%;font-family:Adobe 繁黑體 Std B;">English</a></br>

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

        <br /><br /><br />
                <div style="width:775px;-webkit-text-size-adjust:none;font-size:13px;" 
                    class="style1">
                    理念：<br />
                    <br />
                    <span class="style2">分享</span>藝術 | 讓分享變<span class="style2">藝術</span><br />
                    <br />
                    <br />
                    BeARTY.hk是一個具有不同類別的藝術作品展覽及文化交流平台。我們的目標是要建立一個全港性的藝術資料庫和交流空間，一方面鼓勵市民參於更多與文化有關的活動，提高香港市民整體的文化意識水平。<br />
                    <br />
                    「分享」<br />
                    <br />
                    是近十年新興的詞語，意思是利用不同的平台，向自己身邊的朋友發佈自己喜愛，有趣的內容，不論是生活鎖事，還是終身大事，大家都熱愛「分享」，這是香港，甚至全球的一個生活習慣。<br />
                    <br />
                    「藝術」<br />
                    <br />
                    與我們的日常生活息息相關，我們身邊有很多不同類型，大大小小的藝術，只要細心觀察身邊事物，你會發現自己與「藝術」的距離一點也不遠。<br />
                    <br />
                    <br />
                    你是什麼身分?<br />
                    <br />
                    藝術家<br />
                    <br />
                    你們可以藉著發佈自己的新作品，不論是雕塑、圖畫、短片，甚至是旅遊文章，都能夠在beArty分享，讓更多人認識自己的作品和理念。<br />
                    <br />
                    大眾市民<br />
                    <br />
                    除了能得到藝術家最新的消息外，透過beArty內的月曆表，你可以知道最近最多人參加的展覽，方便忙碌的香港人即使沒有時間親身參於，亦能有透過別人分享的照片，觀賞到展覽內的情況。<br />
                    <br />
                    學生<br />
                    <br />
                    在眾多範疇中，學院這一列正正是提供給正在修讀有關任何藝術科目的你使用的，在beArty向大眾展示自己的功課作品，一方面吸取各方的意見進行改善，另一方面提早為自己建立一個網上的作品集，作為日後尋找工作時的基礎。<br />
                    <br />
                    企業<br />
                    <br />
                    beArty的特色之一是每個人都可以擁有個人的作品集，即是你可以透過那個人的作品集知道他/她曾經發表過的作品，了解或認同他/她的設計概念，並可以主動作出聯絡，提供更多工作機會給對方。
                </div>
                <br /><br /><br /><br />
                <div style="text-align:center;">
                &nbsp;</div>
            <br/><br/>
            <br/><br/><br/><br/><br/><br/><br/><br/>
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
</div>
<div class="wrapper">
<div class="footer"></br>© beARTY. All rights reserved.</br>All images are © each office/photographer mentioned.

</div>
</div>
</form>
</body>
</html>
