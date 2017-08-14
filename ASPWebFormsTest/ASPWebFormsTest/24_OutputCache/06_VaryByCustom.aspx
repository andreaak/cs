<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_VaryByCustom.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._06_varyByCustom" %>

<%--
    Используя параметр VaryByCustom можно определить пользовательский ключ, 
    по которому будет происходить кэширование.
    При использовании данного параметра необходимо добавить в класс Global.asax 
    метод GetVaryByCustomString(HttpContext context, string custom)
--%>

<%@ OutputCache Duration="60" VaryByCustom="device" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Эта страница попала в кэш в
            <%= DateTime.Now.ToLongTimeString() %></h1>
        </div>
    </form>
</body>
</html>
