<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
   Inherits="System.Web.Mvc.ViewPage" Title="Tadmap - Upload Image"
   Theme="Tad" %>

<%@ Register TagPrefix="fjx" Namespace="com.flajaxian" Assembly="com.flajaxian.FileUploader" %>
<%@ Register TagPrefix="fjx" Namespace="com.flajaxian" Assembly="com.flajaxian.DirectAmazonUploader" %>
<%@ Register TagPrefix="tad" Namespace="Tadmap" Assembly="Tadmap.Website" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

   <script runat="server">
      protected void FileNameDetermining(object sender, com.flajaxian.FileNameDeterminingEventArgs args)
      {
         args.FileName = Guid.NewGuid() + System.IO.Path.GetExtension(args.FileName);

      }
      private void FileUploader1_ConfirmUpload(object sender, ConfirmUploadEventArgs e)
      {
         (ViewContext.Controller as Tadmap.Controllers.UploadController).ConfirmUpload(HttpContext.Current.User, e.Name, e.ChangedName);
      }
      
      
   </script>

   <form runat="server">
   <fjx:FileUploader ID="FileUploader1" runat="server">
      <Adapters>
         <%--<fjx:DirectAmazonUploader AccessKey="1RYDPTK2VKP6739SPGR2" SecretKey="FCbtO3UEUp7/5Fql3L57n1cA+d5OEnVP88EsDqJ7"
            BucketName="tadtestus" FileAccess="Private" OnFileNameDetermining="FileNameDetermining"
            OnConfirmUpload="FileUploader1_ConfirmUpload" />--%>
         <tad:LocalUploadAdapter OnFileNameDetermining="FileNameDetermining" OnConfirmUpload="FileUploader1_ConfirmUpload"  />
      </Adapters>
   </fjx:FileUploader>
   </form>
</asp:Content>
