<%@ Page Language="C#" Culture = "en-US" AutoEventWireup="true" CodeFile="share_dialogue.aspx.cs" Inherits="share_dialogue" ValidateRequest="true" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>beARTY</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="img/favicon.ico" />
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
                <a href="share_cn.aspx" style="position:absolute;left:77.2%;top:83%;">中文</a></br>
                
                <asp:LinkButton ID="logout_lnk" runat="server" visible="false" OnClick="logout_lnk_OnClick" style="position:absolute;left:95.75%;top:68%;">logout</asp:LinkButton>
                <a href="advertise.aspx" style="position:absolute;left:89.7%;top:83%;">advertise with us</a>
                </br>
                <a href="about.aspx" style="position:absolute;left:77.1%;top:98%;">about beARTY</a>
                <a href="contact.aspx" style="position:absolute;left:93.7%;top:98%;">contact us</a>
                

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
                    <asp:Button ID="architecture" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="architecture"/>
                    <asp:Button ID="art" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="art"/>
                    <asp:Button ID="design" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="design"/>
                    <asp:Button ID="film" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="film"/>
                    <asp:Button ID="photography" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="photography"/>
                    <asp:Button ID="institutions" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="institutions"/>
                    <asp:Button ID="travel" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="travel"/>
                    <asp:Button ID="events" runat="server" CssClass ="cat_white" OnClick="sort_btn_OnClick" Text="event"/>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
        <div class="share_post" style="width:754px;"> 
            <div style="width:220px;float:right;">
                <div style="height:30px;"></div>
                <asp:CheckBoxList ID="cat_CheckBox" runat="server" >
                    <asp:ListItem>architecture</asp:ListItem>
                    <asp:ListItem>art</asp:ListItem>
                    <asp:ListItem>design</asp:ListItem>
                    <asp:ListItem>film</asp:ListItem>
                    <asp:ListItem>photography</asp:ListItem>
                    <asp:ListItem>institution</asp:ListItem>
                    <asp:ListItem>travel</asp:ListItem>
                    
                </asp:CheckBoxList>
                <asp:CheckBoxList ID="event_chkbox" runat="server" OnSelectedIndexChanged="event_chkbox_OnSelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem>event</asp:ListItem>                    
                </asp:CheckBoxList>
                <asp:CheckBoxList ID="own_chkbox" runat="server" >
                    <asp:ListItem>your own work</asp:ListItem> 
                </asp:CheckBoxList>
                <asp:Label ID="Label1" runat="server" width="220px" style="-webkit-text-size-adjust:none;font-size:11px;word-wrap:break-word;padding-left:20px;">- checking this box will affect your personal post sorting</asp:Label>

                
                <br /><br />
                

                    </br>
                    </br>
                    </br>
                    </br>
            </div>


            <h1 style="color:red;">Posting Dialogue</h1>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:40px">
                Title:
                </div>
                <div ID="elb" style="height:40px;" runat="server" Visible="false">
                <asp:Label ID="eventDate_lb" runat="server" Text="Event date: " Visible="false"/>
                </div>
                <div style="height:30px;">
                <asp:Label ID="relb" runat="server" Text="Related event:"/>
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                cover photo&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="f_upload" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_btn" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" OnClick="upload_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="img_cover" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_status" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:40px">
                <asp:TextBox ID="title" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px" style="padding-left:3px;"></asp:TextBox>
                </div>
                <div ID="etb" style="height:40px;" runat="server" Visible="false">
                <asp:Textbox id="eventDate_tb" runat="server" Visible="false" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px" BackColor="#FF6A6A" style="cursor:text;padding-left:3px;">Click here to select a date.</asp:Textbox>
                </div>
                <div style="height:40px">
                <asp:TextBox ID="retb" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px" style="padding-left:3px;"></asp:TextBox></br>
                <asp:Label ID = "redesclb" runat="server" Font-Size="XX-Small">type in related event name or link</asp:Label>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>
            
            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 1&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload1" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn1" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='1' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton1" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status1" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle1" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor1" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 2&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload2" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn2" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='2' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton2" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status2" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle2" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor2" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <asp:Panel ID="uploadmore_panel" runat="server" Visible="false">
            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 3&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload3" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn3" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='3' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton3" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status3" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle3" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor3" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 4&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload4" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn4" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='4' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton4" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status4" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle4" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor4" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 5&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload5" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn5" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='5' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton5" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status5" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle5" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor5" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 6&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload6" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn6" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='6' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton6" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status6" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle6" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor6" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 7&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload7" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn7" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='7' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton7" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status7" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle7" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor7" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 8&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload8" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn8" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='8' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton8" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status8" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle8" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor8" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:50px">
                File:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                photo 9&emsp;max 4 MB (support jpg, jpeg, png)
                </div>
                <div style="height:50px">
                <asp:FileUpload id="other_upload9" type="file" width = "300px" runat="server" />
                <asp:Button ID="upload_other_btn9" runat="server" Text="upload" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" CommandArgument='9' CommandName="upload_other" OnClick="upload_other_btn_OnClick" style="cursor:pointer"/>
                </br><asp:ImageButton ID="ImageButton9" ImageUrl="" runat="server" style="height:20px;" Visible ="false"/><asp:Label ID = "upload_other_status9" runat="server" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="subtitle9" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="TextAreaEditor9" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            </asp:Panel>

            <div style="float:left;">
            <div class="share_lbody">
                <div style="height:30px">
                
                </div>
                <div style="height:30px">
                Link:
                </div>
                <div style="height:30px">
                Caption:
                </div>
                <div style="height:250px">
                Description:
                </div> 
            </div>

            <div class="share_rbody">
                <div style="height:25px">
                youtube video
                </div>
                <div style="height:30px">
                <asp:TextBox ID="yt" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                <asp:Button ID="yt_btn" runat="server" Text="add" BackColor="White" BorderStyle="None"  Width="70px" Height="30px" OnClick="yt_btn_OnClick" style="cursor:pointer"/>
                </div>
                <div style="height:30px">
                <asp:TextBox ID="yt_title" runat="server" Width="300px" BorderColor="OrangeRed" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                </div>
                <div style="height:250px">
                <asp:TextBox ID="yt_content" TextMode="MultiLine" runat="Server" Height="200px" Width="296px" BorderColor="OrangeRed" ></asp:TextBox>
                </br>
                </div>

            </div>
            </div>

            <div class="post"> 

            <div class="share_lbody" style="width:500px;text-align:center;">
            <asp:Button ID="share_btn" runat="server" Text="submit" CssClass="content_btn" OnClick="share_btn_OnClick"
                onClientClick="if(!confirm('Confirm to share the post.'))return false;"/>
            <asp:Button ID="clear_btn" runat="server" Text="clear" CssClass="content_btn" OnClick="clear_btn_OnClick"
                onClientClick="if(!confirm('Confirm to clear all uploaded photos and content.'))return false;"/>
            <asp:Button ID="uploadmore_btn" runat="server" Text="upload more..." CssClass="content_btn" OnClick="uploadmore_btn_OnClick"/>
            </br></br>
            </div>

            </div>

        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM [ba_post]"></asp:SqlDataSource>
        </div>

        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
       
      </div>

      <div class="siderbar">
             <div style="margin-top:-9px;width:260px;">
                <asp:Button ID="login_btn" runat="server" CssClass ="content_btn" OnClick="login_btn_OnClick" Text="login" style="margin-left:0%;"/>

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
    
</body>
</html>