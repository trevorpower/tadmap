<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true" 
   Inherits="System.Web.Mvc.ViewPage" Title="Tadmap - Sign In" Theme="Tad" %>

<%@ Register Assembly="DotNetOpenId" Namespace="DotNetOpenId.RelyingParty" TagPrefix="RP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script type="text/javascript" id="__openidselector" src="https://www.idselector.com/selector/09e5b013996c710abeb9547fccb8b638a5699b13"
      charset="utf-8"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <% using (MvcForm form = Html.BeginForm())
      { %>
   <div style="margin-left: auto; margin-right: auto; width: 540px; padding-top: 40px;
      padding-bottom: 40px;">
      <div style="text-align: center;">
         <div class="Instructions">
            You can sign into Tadmap by using any <a href="http://openid.net/what/">
               OpenID</a>.<br />
         </div>
         <div class="Instructions">
            Don't have an OpenID? Find out how to <a href="http://openid.net/get/">get one</a>.<br />
            <br />
         </div>
         <asp:Image ID="Image1" runat="server" Style="margin-left: auto; margin-right: auto;"
            ImageUrl="~/App_Themes/Tad/Login/openid-logo-small.png" />
      </div>
      <br />
      <div style="border: solid 0px red; vertical-align: top;">
         <%= Html.TextBox("openid_url", null, new { Class = "OpenIdUrlBox", Size = "40" })%>
         <input type="submit" value="Sign In" class="LoginInButton" />
      </div>
      <span class="ErrorMessage">
         <%= ViewData["LoginErrorMessage"] %></span>
   </div>
   <% } %>
</asp:Content>
