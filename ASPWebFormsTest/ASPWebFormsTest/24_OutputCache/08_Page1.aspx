<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_Page1.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._08_Page1" %>

<%@ Register Src="08_UserControl.ascx" TagPrefix="uc1" TagName="UserControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page 1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:UserControl runat="server" id="MyUserControl" />
        </div>
    </form>
</body>
</html>
