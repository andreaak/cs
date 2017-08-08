<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_AXDFileTest.aspx.cs" Inherits="ASPWebFormsTest._16_Handlers._06_AXDFileTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование AXD файлов</title>
    <style>
        strong {
            color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Images/">Images/</a>
            <br />
            <a href="Images/view.axd">Images/<strong>view.axd</strong></a>
            <br />
            <a href="Images/Subfolder/view.axd">Images/Subfolder/<strong>view.axd</strong></a>
        </div>
    </form>
</body>
</html>
