<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true" CodeFile="Index.aspx.cs"
   Inherits="Index" Title="Tadmap" Theme="Tad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
      <Scripts>
         <asp:ScriptReference Path="~/Scripts/jquery-1.2.6.min.js" />
         <asp:ScriptReference Path="~/Scripts/jquery.jeditable.mini.js" />
      </Scripts>
   </asp:ScriptManager>--%>
   <span class="EditTitle ItemTitle"><%= ViewData["Title"] %></span>
   <div runat="server" id="panelImage" class="ImagePanel">
      <asp:Image runat="server" ID="m_imgPicture" CssClass="ItemDetailImage" BorderWidth="2px" />
      <div class="ImageButtons">
         <asp:HyperLink runat="server" ID="DownloadOriginal">Original</asp:HyperLink>
         <%= Html.ActionLink("Create Tileset", "CreateTileset") %>
         <%--<%= Html.ActionLink("View Tileset", "ViewTileset")%>--%>
      </div>
   </div>
   <div class="ItemDetail">
      <span class="EditDescription MapDescriptionText"><%= ViewData["Description"] %></span>
   </div>
   <div runat="server" id="AdministratorControls">
      <%= Html.ActionLink("Create Tileset", "Mark") %>
      <%= Html.ActionLink("Create Tileset", "UnMark") %>
   </div>
   <div runat="server" id="OwnerControls">
      <%= Html.CheckBox("Public", Convert.ToBoolean(ViewData["IsPublic"]) , new { Class = "PrivacyCheckBox", id="privacyCheckBox" }) %>
      <div style="font-size: 12px;">
         <asp:Label ID="PrivacyStatus" CssClass="PrivacyStatus" runat="server"></asp:Label>
      </div>
   </div>
</asp:Content>
