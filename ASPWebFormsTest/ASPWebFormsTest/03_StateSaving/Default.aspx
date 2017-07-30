<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPWebFormsTest._03_StateSaving.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ol>
        <li><a href="01_BaseInfo.aspx">Base Info</a></li>
        <li>View State
           
            <ul>
                <li><a href="02_ViewState/01_StateStorageProblem.aspx">State Storage Problem</a></li>
                <li><a href="02_ViewState/02_BaseInfo.aspx">Base Info</a></li>
                <li><a href="02_ViewState/03_ViewStateUsing.aspx">View State Using</a></li>
                <li><a href="02_ViewState/04_ViewStateSaveObject.aspx">Save Object</a></li>
            </ul>
        </li>
        <li><a href="03_URL/01_CatalogPage.aspx">URL</a></li>
        <li>Cookies
           
            <ul>
                <li><a href="04_Cookies/01_Cookie.aspx">Base Info</a></li>
                <li><a href="04_Cookies/02_CookieRead.aspx">Cookie Read</a></li>
                <li><a href="04_Cookies/03_CookieExpires.aspx">Cookie Expires</a></li>
                <li><a href="04_Cookies/04_CookieClear.aspx">Cookie Clear</a></li>
            </ul>
        </li>
        <li>Session
           
            <ul>
                <li><a href="05_Session/01_SessionWrite.aspx">Base Info</a></li>
                <li><a href="05_Session/02_SessionRead.aspx">Session Read Write</a></li>
            </ul>
        </li>
        <li>Application
           
            <ul>
                <li><a href="06_Application/01_ApplicationUsing.aspx">Base Info</a></li>
                <li><a href="06_Application/02_ApplicationLock.aspx">Appplication Lock</a></li>
            </ul>
        </li>
    </ol>
</body>
</html>
