<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09_UpdateProgressCancel.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._09_UpdateProgressCancel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpdateProgress Cancel Button Sample</title>

    <script type="text/javascript">
        var prm;

        window.onload = function () {
            prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
        }

        // Функция, которая активирует возможность отмены обратного запроса.
        function InitializeRequest(semder, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
        }

        // Функция для отмены обратного запроса.
        function AbortPostBack() {
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" Text="Date: " runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <img src="ajax-loader.gif" />
                    <a href="javascript:AbortPostBack();">Отмена</a>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            <asp:Button ID="Button1" Text="Обновить" runat="server" OnClick="Button_Click" />
        </div>
    </form>
</body>
</html>
