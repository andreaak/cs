<%@ Page Language="C#" AutoEventWireup="true" CodeFile="02_UserControlTest.aspx.cs"
    Inherits="ASPWebFormsTest._21_UserControls._02_UserControlTest" %>

<!--Регистрация пользовательского элемента управления-->
<%@ Register TagName="_02_UserControl" TagPrefix="test" Src="02_UserControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple UserControl</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!--Создание пользовательского элемента управления-->
            <test:_02_UserControl runat="server" ID="WebUserControl1" />
        </div>
    </form>
</body>
</html>
