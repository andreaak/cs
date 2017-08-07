<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_UpdatePanelHandledError.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._03_UpdatePanelHandledError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpdatePanel ExceptionSample #2</title>
    <script type="text/javascript">

        //  метод pageLoad вызывается автоматически если присутствует в сценарии
        function pageLoad() {
            var pageManager = Sys.WebForms.PageRequestManager.getInstance();
            // Событие endRequest происходит по завершению каждой асинхронной отправки запроса.
            pageManager.add_endRequest(endRequest);
        }

        // Обработчик, который проверяет наличие ошибки, отображает ее, если она есть, и устанавливает статус ошибки, как обработанный.
        function endRequest(sender, args) {
            if (args.get_error() != null) {
                $get("ErrorOutput").innerHTML = args.get_error().message;
                args.set_errorHandled(true);
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button1" Text="Обновить" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <span id="ErrorOutput" style="background-color:Red; color:White;"></span>
    </div>
    </form>
</body>
</html>
