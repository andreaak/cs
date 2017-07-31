<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="11_ImageMap.aspx.cs" 
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._11_ImageMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Map #1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:ImageMap ID="ImageMap1" ImageUrl="Images/map.jpg" runat="server">

            <asp:CircleHotSpot HotSpotMode="Navigate" NavigateUrl="http://example.com?circle"
                Radius="55" X="110" Y="235" />

            <asp:RectangleHotSpot HotSpotMode="Navigate" NavigateUrl="http://example.com?rectangle"
                Left="53" Right="325" Top="41" Bottom="110" />

            <asp:PolygonHotSpot HotSpotMode="Navigate" NavigateUrl="http://example.com?polygon"
                Coordinates="217,300,262,251,345,272,318,338,249,341" />

        </asp:ImageMap>
    </form>
</body>
</html>
