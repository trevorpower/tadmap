﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" CodeBehind="Index.aspx.cs"
   Theme="Tad" Inherits="TadMap_MVC.Views.Home.Index" Title="Tadmap - Image hosting for map collectors" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div id="divMapList" style="padding: 10px 30px 10px 30px;">
      <div class="NoteBox">
         <p>
            You are browsing all <b>public</b> images.
         </p>
         <p id="LoginText">
            <span id="SignInText">
               <%= Html.ActionLink("Sign in", "Login", "Account") %>
               to view your own <i>private</i> images or <i>upoload</i> new images.</span>
         </p>
      </div>
      <% Html.RenderPartial("ImageListControl", ViewData.Model ); %>
   </div>
</asp:Content>
