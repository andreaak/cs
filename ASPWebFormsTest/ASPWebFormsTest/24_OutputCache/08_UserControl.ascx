<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="08_UserControl.ascx.cs" Inherits="ASPWebFormsTest._24_OutputCache._08_UserControl" %>

<%--
    Разметка данного контрола кэшируется на 15 секунд.
    Параметр Shared="true" указывает, что разметка данного элемента управления 
    будет общей для всех страниц, которые его исопльзуют. 
--%>

<%@ OutputCache Duration="15" VaryByParam="None" Shared="false" %>

<h1>Разметка UserControl'а кэширована в  
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" />
</h1>
