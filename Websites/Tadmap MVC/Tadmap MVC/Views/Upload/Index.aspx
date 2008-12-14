<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true" CodeFile="Index.aspx.cs"
   Inherits="Tadmap_MVC.Views.Upload.Index" Title="Tadmap - Upload Image" Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <% using (Html.BeginForm("Index", "Upload", FormMethod.Post, new { enctype = "multipart/form-data" }))
      { %>
   <div class="ItemDetailListEdit">
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">Title</span>
         <%= Html.TextBox("title", null, new { Class = "ItemDetailControl" })%>
      </div>
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">Description</span>
         <%= Html.TextBox("description", null, new { Class = "ItemDetailControl", MaxLength = "1024" })%>
         <%--<asp:TextBox ID="m_txtDescription" runat="server" CssClass="ItemDetailControl" MaxLength="1024"
            TextMode="MultiLine"></asp:TextBox>--%>
      </div>
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">File</span>
         <input id="file" name="file" type="file" class="ItemDetailControl" />
      </div>
   </div>
   <div style="margin-left: auto; margin-right: auto; clear: both; text-align: center;
      padding: 12px;">
      <%= Html.ActionLink("Upload", "Upload", null, new { Class = "Button", style = "width: 80px;" })%>
      <input type="submit" value="Sign In" class="LoginInButton" />
   </div>
   <% } %>
</asp:Content>
