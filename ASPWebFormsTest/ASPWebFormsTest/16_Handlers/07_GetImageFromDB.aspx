<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_GetImageFromDB.aspx.cs" Inherits="GetImagesFromDb._07_GetImageFromDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="07_GetImageFromDBHandler.ashx?image=5" alt="Image" />
            <img src="07_GetImageFromDBHandler.ashx?image=6" alt="Image" />
            <img src="07_GetImageFromDBHandler.ashx?image=7" alt="Image" />
        </div>
    </form>
</body>
</html>
