<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPWebFormsTest._02_Page.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ol>
        <li><a href="01_BaseInfo.aspx">Base Info</a></li>
        <li><a href="02_PageLifeCycle.aspx">Page Life Cycle</a></li>
        <li>Page events
            <ul>
                <li><a href="03_PageEventHandling/01_EventHandlingWireup.aspx">Event Wireup</a></li>
                <li><a href="03_PageEventHandling/02_EventHandlingPageEvents.aspx">Page Events</a></li>
                <li><a href="03_PageEventHandling/03_EventHandlingOverrideHandlers.aspx">Override Handlers</a></li>
            </ul>
        </li>
        <li><a href="04_IsPostBackProp.aspx">IsPostBack</a></li>
        <li>Request
            <ul>
                <li><a href="05_Request/01_BaseInfo.aspx">Base Info</a></li>
                <li><a href="05_Request/02_QueryString/01_RequestWithQueryString.aspx">Query String</a></li>
                <li><a href="05_Request/03_Form/01_RequestWithPostMethod.htm">Form</a></li>
                <li><a href="05_Request/03_Form/02_RequestWithGetMethod.htm">Form with GET method</a></li>
                <li><a href="05_Request/03_Form/06_DataTransferBetweenPagesChangeFormAction.aspx">
                    Data Transfer Between Pages Witn Changing for Action
                </a></li>
            </ul>
        </li>
        <li>Response
            <ul>
                <li><a href="06_Response/01_BaseInfo.aspx">Base Info</a></li>
                <li><a href="06_Response/02_ResponseWrite.aspx">Response Write</a></li>
                <li><a href="06_Response/03_ResponseWriteFile.aspx">Response WriteFile</a></li>
                <li><a href="06_Response/05_ResponseRedirect/05_ResponseRedirect.aspx">Response Redirect</a></li>
            </ul>
        </li>
        <li>Server
            <ul>
                <li><a href="07_Server/01_BaseInfo.aspx">Base Info</a></li>
                <li><a href="07_Server/02_MapPath.aspx">Map Path</a></li>
                <li><a href="07_Server/03_HtmlEncode.aspx">Html Encode</a></li>
                <li><a href="07_Server/04_UrlEncode.aspx">Url Encode</a></li>
            </ul>
        </li>


    </ol>
</body>
</html>
