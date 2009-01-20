<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageListControl.ascx.cs"
   Inherits="Tadmap_MVC.Views.Shared.ImageListControl" %>
<% 
   Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper s3 = new Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper(TadMap.Configuration.S3Storage.AccessKey, TadMap.Configuration.S3Storage.SecretAccessKey);
   foreach (Tadmap_MVC.Models.Images.TadmapImage image in ViewData.Model)
   {
%>
<div class="ImageListItem" onclick="window.location = '<%= Url.Action("Index", "Image", new { id = image.Id }) %>'"
   onmouseout="this.style.background = '#FFFFFF';" onmouseover="this.style.background = '#FFFFCC';">
   <img alt="<%= image.Title %>" src="<%= s3.GetUrl(TadMap.Configuration.S3Storage.BucketName, "Square_" + image.Key) %>"
      width="80" height="80" style="float: left; margin-right: 5px;" />
   <div class="TextArea">
      <span>
         <%= Html.ActionLink(image.Title, image.Id.ToString(), "Image") %></span> <span class="ImageListItemDescription">
            <%= image.Description %></span>
   </div>
</div>
<% } %>