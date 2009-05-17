<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs"
   Inherits="Tadmap.Views.Shared.NavigationBar" %>
<%
   if (Request.IsAuthenticated)
   {
%>
   <%= Html.ActionLink("My Maps", "Index", "UserImages", null, new { @class = "MasterPageNavigationLink" }) %>
<%
   }
    else
    {
%><%
   }
%>