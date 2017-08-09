<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_UserControlDataBoundTest.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls.DataBinding._08_UserControlDataBoundTest" %>

<%@ Register Src="08_UserControlDataBound.ascx" TagName="UserControlDataBounded" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataBound Control</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <uc1:UserControlDataBounded ID="UserControlDataBounded1" runat="server"
                DataFirstNameField="A" DataLastNameField="B" DataMiddleNameField="C" />

        </div>
    </form>
</body>
</html>
