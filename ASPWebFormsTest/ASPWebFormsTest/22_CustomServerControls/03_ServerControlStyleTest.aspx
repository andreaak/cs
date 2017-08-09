<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ServerControlStyleTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._03_ServerControlStyleTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <cc1:_03_ServerControlStyle ID="MyWebControl1" runat="server"
                BackColor="Green" ForeColor="White" Height="30px" />

        </div>
    </form>
</body>
</html>
