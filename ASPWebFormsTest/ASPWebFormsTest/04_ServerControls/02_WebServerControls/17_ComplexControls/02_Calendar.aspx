<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_Calendar.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls._02_Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar #1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Calendar SelectionMode="DayWeekMonth" runat="server" ID="Calendar1" BackColor="#FFFFCC" BorderColor="#FFCC66"
                BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                ForeColor="#663399" Height="200px" Width="220px" ShowGridLines="True">
                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                <OtherMonthDayStyle ForeColor="#CC9966" />
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                <SelectorStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
            </asp:Calendar>
            <asp:Button Text="Дата" runat="server" OnClick="Button_Click" />
            <p>
                <asp:Label ID="OutputLabel" runat="server" EnableViewState="false" />
            </p>
        </div>
    </form>
</body>
</html>
