<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
   CodeFile="Index.aspx.cs" Inherits="Index" Title="Tadmap" Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
      <Scripts>
         <asp:ScriptReference Path="~/Scripts/jquery-1.2.6.min.js" />
         <asp:ScriptReference Path="~/Scripts/jquery.jeditable.mini.js" />
      </Scripts>
   </asp:ScriptManager>--%>
   <span class="EditTitle ItemTitle">
      <%= ViewData["Title"] %></span>
   <div runat="server" id="panelImage" class="ImagePanel">
      <asp:Image runat="server" ID="m_imgPicture" CssClass="ItemDetailImage" BorderWidth="2px" />
      <% if (Convert.ToBoolean(ViewData["CanEdit"]))
         { %>
      <div class="ImageButtons">
         <%= Html.ActionLink("Original", "Original", "Image", new { id = "DownloadOriginal" ), new { id = "DownloadOriginal" )%>
         <asp:HyperLink runat="server" ID="DownloadOriginal">Original</asp:HyperLink>
         <%= Html.ActionLink("Create Tileset", "CreateTileset")%>
         <%--<%= Html.ActionLink("View Tileset", "ViewTileset")%>--%>
      </div>
      <% } %>
   </div>
   <div class="ItemDetail">
      <span class="EditDescription MapDescriptionText">
         <%= ViewData["Description"] %></span>
   </div>
   <% if (HttpContext.Current.User.IsInRole(TadMap.Security.TadMapRoles.Administrator))
      { %>
   <%= Html.ActionLink("Mark", "Mark") %>
   <%= Html.ActionLink("Un-Mark", "UnMark") %>
   <%} %>
   <% if (Convert.ToBoolean(ViewData["CanEdit"]))
      { %>
   <%= Html.CheckBox("PublicCheckBox", Convert.ToBoolean(ViewData["IsPublic"]) , new { Class = "PrivacyCheckBox" }) %><label for="PublicCheckBox">Public</label>
   <div style="font-size: 12px;">
      <span id="PrivacyStatus" class="PrivacyStatus"><%= Convert.ToBoolean(ViewData["IsPublic"]) ? "<b>Anyone</b> can view this image." : "<b>Only you</b> can view this image." %></span>
   </div>
   <%} %>
</asp:Content>
