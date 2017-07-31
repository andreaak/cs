<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ImagesUsing.aspx.cs" 
    Inherits="ASPWebFormsTest._05_PageDesign._02_Themes._03_ImagesUsing" 
    Theme="MyTheme" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование ASP.NET темы #4</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ImageButton ID="ImageButton1" runat="server" SkinID="OkButton" />
        <asp:ImageButton ID="ImageButton2" runat="server" SkinID="CancelButton" />
    </div>
    </form>
</body>
</html>
