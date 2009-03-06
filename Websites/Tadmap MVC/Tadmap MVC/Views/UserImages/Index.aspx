<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
   CodeBehind="Index.aspx.cs" Inherits="Tadmap.Views.UserImages.Index" Title="Tadmap - My Images"
   Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div  style="padding: 10px 30px 10px 30px;">
      <div class="NoteBox">
         <p>
            You are browsing all <b>your</b> images.</p>
         <p>
            <%= Html.ActionLink("Upload Image", "Index", "Upload", null, new { @class = "" }) %></p>
      </div>
      <% 
         Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper s3 = new Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper(Tadmap.Configuration.S3Storage.AccessKey, Tadmap.Configuration.S3Storage.SecretAccessKey);
         foreach (Tadmap.TadImage image in ViewData["ImageList"] as IEnumerable)
         {%>
      <div class="ImageListItem" onclick="window.location = '<%= Url.Action("Index", "Image", new { id = image.Id }) %>'" onmouseout="this.style.background = '#FFFFFF';"
         onmouseover="this.style.background = '#FFFFCC';">
         <img alt="<%= image.Title %>" src="<%= s3.GetUrl(Tadmap.Configuration.S3Storage.BucketName, "Square_" + image.StorageKey) %>"
            width="80" height="80" style="float: left; margin-right: 5px;" />
         <div class="TextArea">
            <%= Html.ActionLink(image.Title, image.Id.ToString(), "Image") %>
            <span class="ImageListItemDescription">
               <%= image.Description %></span>
         </div>
      </div>
      <%} %>
   </div>
</asp:Content>
