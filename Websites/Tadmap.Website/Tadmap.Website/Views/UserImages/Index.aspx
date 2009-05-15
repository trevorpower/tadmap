<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
   Inherits="System.Web.Mvc.ViewPage< List< Tadmap.Model.Image.TadmapImage > >"  Title="Tadmap - My Images" Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div style="padding: 10px 30px 10px 30px;">
      <div class="NoteBox">
         <p>
            You are browsing all <b>your</b> images.</p>
         <p>
            <%= Html.ActionLink("Upload Image", "Index", "Upload", null, new { @class = "" }) %></p>
      </div>
      <% Html.RenderPartial("ImageListControl", ViewData.Model ); %>
   </div>
</asp:Content>
