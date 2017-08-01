<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_FileUploader.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls._04_FileUploaderSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FileUploader Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload runat="server" ID="fileUploader" />
            <asp:Button ID="ButtonUpload" Text="Upload" runat="server" OnClick="UploadButton_Click" />
            <br />
            <asp:Label ID="LabelForMessage" Text="" runat="server" />
        </div>
    </form>
</body>
</html>
