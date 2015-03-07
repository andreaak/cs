<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="TestForm3.aspx.cs" Inherits="ASPTest._00_Forms._05_PageRedirection.TestForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <h1>Start Form</h1>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged">FGH</asp:TextBox>
    </div>
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
            </asp:DropDownList>


        </div>

    <asp:button runat="server" text="Go"/>
    </form>
</body>
</html>
