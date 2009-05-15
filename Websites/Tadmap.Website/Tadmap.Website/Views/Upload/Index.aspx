﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
   Inherits="System.Web.Mvc.ViewPage<com.flajaxian.FileUploaderAdapter>" Title="Tadmap - Upload Image"
   Theme="Tad" %>

<%@ Register TagPrefix="fjx" Namespace="com.flajaxian" Assembly="com.flajaxian.FileUploader" %>
<%@ Register TagPrefix="fjx" Namespace="com.flajaxian" Assembly="com.flajaxian.DirectAmazonUploader" %>
<%@ Register TagPrefix="tad" Namespace="Tadmap" Assembly="Tadmap.Website" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

   <script src="../../Scripts/jquery-1.2.6.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

   <script runat="server">
      public void FileUploader1_FileNameDetermining(object sender, com.flajaxian.FileNameDeterminingEventArgs args)
      {
         args.FileName = Guid.NewGuid() + System.IO.Path.GetExtension(args.FileName);

      }
      public void FileUploader1_ConfirmUpload(object sender, ConfirmUploadEventArgs e)
      {
         (ViewContext.Controller as Tadmap.Controllers.UploadController).ConfirmUpload(HttpContext.Current.User, e.Name, e.ChangedName);
      }

      protected override void OnPreLoad(EventArgs e)
      {
         foreach (System.Reflection.EventInfo ev in Model.GetType().GetEvents())
         {
            ev.AddEventHandler(Model, Delegate.CreateDelegate(ev.EventHandlerType, this, "FileUploader1_" + ev.Name, false, false));
         }
            
         FileUploader1.Adapters.Add(Model);

         base.OnPreInit(e);
      }

   </script>

   <script language="javascript" type="text/javascript">

      var count = 0;

      function keptAlive(val) {
         setTimeout(keepAlive, 120000);
      }

      function keepAlive() {
         count++;
         $.getJSON('<%= Url.RouteUrl( new { controller = "upload", action = "KeepAlive" }) %>' + "/" + count, keptAlive);
      }

      $(document).ready(function() {
         setTimeout(keepAlive, 120000);
      });
   </script>

   <form runat="server">
   <fjx:FileUploader ID="FileUploader1" runat="server">
      <Adapters>
         <%--<fjx:DirectAmazonUploader AccessKey="1RYDPTK2VKP6739SPGR2" SecretKey="FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7"
            BucketName="tadtestus" FileAccess="Private" OnFileNameDetermining="FileUploader1_FileNameDetermining"
            OnConfirmUpload="FileUploader1_ConfirmUpload" />--%>
         <%-- <tad:LocalUploadAdapter OnFileNameDetermining="FileNameDetermining" OnConfirmUpload="FileUploader1_ConfirmUpload"  />--%>
      </Adapters>
   </fjx:FileUploader>
   </form>
</asp:Content>
