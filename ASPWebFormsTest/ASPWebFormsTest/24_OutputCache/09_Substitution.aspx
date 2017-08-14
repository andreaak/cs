<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09_Substitution.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._09_Substitution" %>

<%@ OutputCache Duration="30" VaryByParam="None" %>

<!DOCTYPE html>
<!--
    Substitution - определяет область на странице, 
    которая может динамически обновляется и добавляться в кэшированную страницу.
    При настройке элемента управления необходимо задать свойство MethodName 
    в котором указать имя метода в коде страницы.
    Метод должен быть статическим принимать HttpContext 
    и возвращать строковое значение.
-->
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

