<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_ImplicitLocalizationInnerObjects.aspx.cs" 
    Inherits="ASPWebFormsTest._12_Localization._04_ImplicitLocalizationInnerObjects" UICulture="ru-RU" Culture="auto" 
    meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridViewLocalization" runat="server" 
            meta:resourcekey="GridViewLocalizationResource1">
            <AlternatingRowStyle BackColor="LightBlue" />
            <RowStyle BackColor="LightGreen" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
