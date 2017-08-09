<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_UserControlEventsTest.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._06_UserControlEventsTest" %>

<%@ Register Src="06_UserControlEvents.ascx" TagName="Calculator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UserControl Events</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--Открытые свойства класса контрола можно инициализировать с помощью атрибутов в aspx
        разметке--%>
            <uc1:Calculator ID="Calculator1" runat="server"
                OnError="Calculator1_OnError" />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" EnableViewState="false" />
        </div>
    </form>
</body>
</html>
