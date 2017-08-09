<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_SimpleServerControlTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._02_SimpleServerControlTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple Specialized Control</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:_02_SimpleServerControl ID="MyControl1" runat="server">
            </cc1:_02_SimpleServerControl>
        </div>
    </form>
</body>
</html>
