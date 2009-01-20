<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs"
   Inherits="Tadmap_MVC.Views.Upload.Index" Title="Tadmap - Upload Image" Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <% using (Html.BeginForm("Upload", "Upload", FormMethod.Post, new { enctype = "multipart/form-data" }))
      { %>
   <div class="ItemDetailListEdit">
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">Title</span>
         <%= Html.TextBox("title", null, new { Class = "ItemDetailControl" })%>
      </div>
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">Description</span>
         <%= Html.TextBox("description", null, new { Class = "ItemDetailControl", MaxLength = "1024" })%>
      </div>
      <div class="ItemDetailEdit">
         <span class="ItemDetailControlLabel">File</span>
         <input id="file" name="file" type="file" class="ItemDetailControl" />
      </div>
   </div>
   <div style="margin-left: auto; margin-right: auto; clear: both; text-align: center;
      padding: 12px;">
      <input type="submit" name="submit" value="Add Map Image" />  
   </div>
   <% } %>
</asp:Content>
