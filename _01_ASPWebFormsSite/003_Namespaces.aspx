<%@ Page Language="C#" %>

<%@ Import Namespace="System.IO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Импортирование пространств имен</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%
            FileInfo info = new FileInfo("C:\\windows\\explorer.exe");
            // Без импортирования пространства имен следует использовать полное имя типа.
            // System.IO.FileInfo info = new System.IO.FileInfo("C:\\windows\\explorer.exe");

            long size = info.Length;
            string output = string.Format("Размер фала explorer.exe <b>{0}</b> байт", size);
            Response.Write(output);
        %>
    </div>
    </form>
</body>
</html>
