<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_SimpleDataBoundControlTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls.DataBinding._02_SimpleDataBoundControlTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls.DataBinding" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataBoundControl</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:_02_SimpleDataBoundControl ID="MessageBoardControl1" runat="server" />
        </div>
    </form>
</body>
</html>
