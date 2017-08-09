<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ControlRegisterInConfigTest.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._03_ControlRegisterInConfigTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--Контрол зарегистрирован в файле web.config и может быть создан на любой странице данного приложения--%>
            <test:_03_ControlRegisterInConfig runat="server" ID="WebUserControl1"></test:_03_ControlRegisterInConfig>
        </div>
    </form>
</body>
</html>
