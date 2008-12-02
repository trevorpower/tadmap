<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginUserControl.ascx.cs" Inherits="Tadmap_MVC.Views.Shared.LoginUserControl" %>
<%
    if (Request.IsAuthenticated) {
       // css style MasterPageAdministrationLink
       //           MasterPageLoginName
%>
        <b><%= Html.Encode(Page.User.Identity.Name) %></b>
        <%= Html.ActionLink("Sign Out", "Logout", "Account") %>
<%
    }
    else {
%> 
        <%= Html.ActionLink("Sign In", "Login", "Account") %>
<%
    }
%>
