<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
   CodeFile="Index.aspx.cs" Theme="Tad" Inherits="Index" Title="Tadmap - Image hosting for map collectors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div runat="server" id="divMapList" style="padding: 10px 30px 10px 30px;">
      <div class="NoteBox">
         <p>
            You are browsing all <b>public</b> images.
         </p>
         <p runat="server" id="LoginText">
            <asp:Label runat="server" ID="SignInText">
               <%= Html.ActionLink("Sign in", "Login", "Account") %>
               to view your own <i>private</i> images or <i>upoload</i> new images.</asp:Label>
         </p>
      </div>
      <% 
         Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper s3 = new Affirma.ThreeSharp.Wrapper.ThreeSharpWrapper(TadMap.Configuration.S3Storage.AccessKey, TadMap.Configuration.S3Storage.SecretAccessKey);
         foreach (UserImage image in ImageList) 
         {
      %>
      <div class="ImageListItem" onclick="window.location = ''" onmouseout="this.style.background = '#FFFFFF';" onmouseover="this.style.background = '#FFFFCC';">
         <img alt="<%= image.Title %>" src="<%= s3.GetUrl(TadMap.Configuration.S3Storage.BucketName, "Square_" + image.Key) %>" width="80" height="80" style="float: left; margin-right: 5px;" />
         <div class="TextArea">
            <%= Html.ActionLink(image.Title, image.Id.ToString(), "Image") %>
            <span class="ImageListItemDescription"><%= image.Description %></span>
         </div>
      </div>
      <% } %>
   </div>
</asp:Content>
