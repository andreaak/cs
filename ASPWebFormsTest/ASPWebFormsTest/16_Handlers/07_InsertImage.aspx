<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_InsertImage.aspx.cs" Inherits="ASPWebFormsTest._16_Handlers._07_InsertImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload runat="server" ID="fileUploader" />
            <asp:Button ID="ButtonUpload" Text="Upload" runat="server" OnClick="ButtonUpload_OnClick"/>
            <br />
            <asp:Label ID="LabelForMessage" Text="" runat="server" />
        </div>
    </form>
</body>
</html>
