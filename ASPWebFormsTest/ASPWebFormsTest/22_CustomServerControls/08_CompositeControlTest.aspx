<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_CompositeControlTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._08_CompositeControlTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CompositeControl Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc1:_08_CompositeControl ID="MyCompositeControl1" runat="server" />
        </div>
    </form>
</body>
</html>

