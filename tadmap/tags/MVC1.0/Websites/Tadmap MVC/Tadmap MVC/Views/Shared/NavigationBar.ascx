<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs"
   Inherits="Tadmap_MVC.Views.Shared.NavigationBar" %>
<%
   if (Request.IsAuthenticated)
   {
%>
   <%= Html.ActionLink("My Maps", "Index", "UserImages", null, new { Class = "MasterPageNavigationLink" }) %>
<%
   }
    else
    {
%><%
   }
%>