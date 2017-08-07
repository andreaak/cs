<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_UpdateProgress.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._08_UpdateProgress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpdateProgress Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" Text="Date: " runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <%--
            Данный блок будет становиться видимым в момент выполнения асинхронного обратного запроса.
            Для того, что бы блок был видимым только при обновлении конкретного UpdatePanel 
            следует установить свойство AssociatedUpdatePanelID
            --%>
            <asp:UpdateProgress runat="server">
                <ProgressTemplate>
                    <img src="ajax-loader.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            <asp:Button ID="Button1" Text="Обновить" runat="server" OnClick="Button_Click" />
        </div>
    </form>
</body>
</html>
