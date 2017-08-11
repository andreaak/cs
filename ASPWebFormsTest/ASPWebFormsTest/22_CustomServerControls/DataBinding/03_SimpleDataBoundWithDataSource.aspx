<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_SimpleDataBoundWithDataSource.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls.DataBinding._03_SimpleDataBoundWithDataSource" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls.DataBinding" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:_02_SimpleDataBoundControl ID="MessageBoardControl1" runat="server"
                DataMessageField="MessageBody"
                DataTitleField="MessageTitle"
                DataSourceID="TestSqlDataSource" />

            <asp:SqlDataSource ID="TestSqlDataSource" runat="server"
                ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT [MessageTitle], [MessageBody] FROM [Messages]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
