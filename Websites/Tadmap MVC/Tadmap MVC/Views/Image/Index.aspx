<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
   CodeFile="Index.aspx.cs" Inherits="Tadmap_MVC.Views.Image.Index" Title="Tadmap"
   Theme="Tad" %>

<asp:Content ContentPlaceHolderID="head" ID="HeadContent" runat="server">

   <script src="../../Scripts/jquery-1.2.6.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <span class="EditTitle ItemTitle">
      <%= ViewData.Model.Title %></span>
   <div class="ImagePanel">
      <img class="ItemDetailImage" src="<%= ViewData.Model.PreviewUrl %>" alt="<%= ViewData.Model.Title %>" />
      <div class="ImageButtons">
         <% if (ViewData.Model.OriginalUrl != null)
            { %>
         <a href="<%= ViewData.Model.OriginalUrl %>">Original</a>
         <%} %>
      </div>
   </div>
   <div class="ItemDetail">
      <span class="EditDescription MapDescriptionText">
         <%= ViewData.Model.Description %></span>
   </div>
   <% if (ViewData.Model.IsEditable)
      { %>
   <%= Html.CheckBox("PublicCheckBox", ViewData.Model.IsPublic, new { Class = "PrivacyCheckBox" })%><label
      for="PublicCheckBox">Public</label>
   <div style="font-size: 12px;">
      <span id="PrivacyStatus" class="PrivacyStatus"><b>
         <%= ViewData.Model.IsPublic ? Tadmap_MVC.Views.Image.ViewResources.Anyone : Tadmap_MVC.Views.Image.ViewResources.OnlyYou %></b>
         <%= Tadmap_MVC.Views.Image.ViewResources.PrivacyStatusMessageSuffix %>
      </span>
   </div>
   <%} %>
   <%
      if (ViewData.Model.ShowOffensiveCount)
      {
         Html.RenderPartial("OffensiveControl", ViewData.Model);
      }
   %>
</asp:Content>
