<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_DataTransferBetweenPagesChangeFormAction.aspx.cs" 
    Inherits="ASPWebFormsTest._02_Page._05_Request._03_Form._06_DataTransferBetweenPagesChangeFormAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Start Form</h1>
            <asp:TextBox ID="TextBox1" runat="server">1111</asp:TextBox>
        </div>
        <div>
            <input type="text" id="TextBox2" name="TextBox2" value="2222" />
        </div>
        <asp:button ID="Button1" runat="server" text="Go"/>
    </form>
</body>
</html>
