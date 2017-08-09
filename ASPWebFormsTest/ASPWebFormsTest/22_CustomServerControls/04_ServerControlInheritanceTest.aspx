<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_ServerControlInheritanceTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._04_ServerControlInheritanceTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom HyperLink</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <cc1:_04_ServerControlInheritanceHyperLink ID="MyHyperLink1" runat="server" NavigateUrl="SomePage.aspx" Text="Click Me!">
            </cc1:_04_ServerControlInheritanceHyperLink>

        </div>
    </form>
</body>
</html>
