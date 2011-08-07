<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginUserControl.ascx.cs" Inherits="Tadmap.Views.Shared.LoginUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        <b class="MasterPageLoginName"><%= Html.Encode(Page.User.Identity.Name) %></b>
        <%= Html.ActionLink("Sign Out", "Logout", "Account", null, new { @class = "MasterPageAdministrationLink" } )%>
<%
    }
    else {
%> 
        <%= Html.ActionLink("Sign In", "Login", "Account", null, new { @class = "MasterPageAdministrationLink" })%>
<%
    }
%>
