<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tag_example.aspx.cs" Inherits="tag_example" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>beARTY</title>
    <link rel="shortcut icon" href="img/favicon.ico" />
    <script type="text/javascript" src="Scripts/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery.lightbox-0.5.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.lightbox-0.5.css" media="screen" />
</head>
<script type="text/javascript">
    $(function () {
        $('a[@rel*=lightbox]').lightBox({ maxWidth: 1024, maxHeight: 1024 });

    });
</script>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left;width:753px;left:25%;top:5%;position:absolute;line-height: 24px;width:550px;">
    <font style='font-size:large;'><b>Tag Example</b></font><br /><br />
    [topic]<font style='color:#FF6A6A;'>beArty</font>[/topic]&emsp;<b><font style='font-size:20px;-webkit-text-size-adjust:none;color:#FF6A6A;'>beArty</font></b> </br>
    [b]<font style='color:#FF6A6A;'>beArty</font>[/b]&emsp;<b>beArty</b> </br>
    [i]<font style='color:#FF6A6A;'>beArty</font>[/i]&emsp;<i>beArty</i> </br>
    [strong]<font style='color:#FF6A6A;'>beArty</font>[/strong]&emsp;<strong>beArty</strong> </br>
    [cite]<font style='color:#FF6A6A;'>beArty</font>[/cite]&emsp;<cite>beArty</cite> </br>
    [code]<font style='color:#FF6A6A;'>beArty</font>[/code]&emsp;<code>beArty</code> </br>
    [em]<font style='color:#FF6A6A;'>beArty</font>[/em]&emsp;<em>beArty</em> </br>
    [strike]<font style='color:#FF6A6A;'>beArty</font>[/strike]&emsp;<strike>beArty</strike> </br>
    [img]<font style='color:#FF6A6A;'>http://bearty.hk/img/lau_logo.png</font>[/img] </br>
    <div style='width:400px;text-align:left;outline : none;'><A rel='lightbox' HREF='http://bearty.hk/img/lau_logo.png' style='outline : none;'><img src='http://bearty.hk/img/lau_logo.png' style='max-width: 400px;min-width: 400px;vertical-align:middle;margin-bottom:5px;margin-top:5px;outline : none;'/></A></div>
    </div>
    </form>
</body>
</html>
