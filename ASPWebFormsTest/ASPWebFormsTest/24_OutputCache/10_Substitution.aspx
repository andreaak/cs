<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="10_Substitution.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._10_Substitution" %>

<%@ OutputCache Duration="30" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Послекэшовая подстановка</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Разметка страницы кэширована в
            <%=DateTime.Now.ToLongTimeString() %></h1>
            <hr />
            <asp:Substitution ID="Substitution" runat="server" MethodName="GetDate" />
            <hr />
        </div>
    </form>
</body>
</html>
