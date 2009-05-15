<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true"
   Inherits="System.Web.Mvc.ViewPage" Title="Tadmap - Upload Image"
   Theme="Tad" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

   <link type="text/css" href="<%= Url.Content("~/SWF/default.css")%>" rel="Stylesheet" />
   
   <script type="text/javascript" src="<%= Url.Content("~/Scripts/swfupload.js") %>"></script>

   <script type="text/javascript" src="<%= Url.Content("~/Scripts/swfuploadhandlers.js") %>"></script>

   <script type="text/javascript">
      var swfu;
      window.onload = function() {
         swfu = new SWFUpload({
            // Backend Settings
            upload_target_url: "../Upload.mvc/Upload", // Relative to the SWF file

            // File Upload Settings
            file_size_limit: "1073741824", // 1GB
            file_types: "*.jpg; *.jpeg; *.tif; *.tiff; *.png",
            file_types_description: "Image Files",
            file_upload_limit: "0",    // Zero means unlimited
            begin_upload_on_queue: true,

            // Event Handler Settings
            file_queued_handler: fileQueued,
            file_progress_handler: fileProgress,
            file_cancelled_handler: fileCancelled,
            file_complete_handler: fileComplete,
            queue_complete_handler: queueComplete,
            //queue_stopped_handler : queueStopped,
            //dialog_cancelled_handler : fileDialogCancelled,
            error_handler: uploadError,

            // Flash Settings
            flash_url: "<%= Url.Content("~/SWF/swfupload.swf") %>", // Relative to this file
            flash_container_id: "theflashgohere",

            // UI Settings
            ui_container_id: "swfu_container",
            degraded_container_id: "degraded_container",

            // Debug Settings
            debug: false
         });
         swfu.addSetting("upload_target", "divFileProgressContainer");
      }
   </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div id="swfu_container" style="display: none; margin: 0px 10px;">
      <div>
         <button id="btnBrowse" type="button" style="padding: 5px;" onclick="swfu.browse(); this.blur();">
            Add Map Image</button>
      </div>
      <div id="divFileProgressContainer" style="height: 75px;">
      </div>
      <div id="thumbnails">
      </div>
   </div>
   <div id="degraded_container">
      <% using (Html.BeginForm("Upload", "Upload", FormMethod.Post, new { enctype = "multipart/form-data" }))
         { %>
      <div class="ItemDetailListEdit">
         <div class="ItemDetailEdit">
            <span class="ItemDetailControlLabel">Title</span>
            <%= Html.TextBox("title", null, new { @class = "ItemDetailControl" })%>
         </div>
         <div class="ItemDetailEdit">
            <span class="ItemDetailControlLabel">Description</span>
            <%= Html.TextBox("description", null, new { @class = "ItemDetailControl", MaxLength = "1024" })%>
         </div>
         <div class="ItemDetailEdit">
            <span class="ItemDetailControlLabel">File</span>
            <input id="file" type="file" class="ItemDetailControl" />
         </div>
      </div>
      <div style="margin-left: auto; margin-right: auto; clear: both; text-align: center;
         padding: 12px;">
         <input type="submit" name="submit" value="Add Map Image" />
      </div>
      <% } %>
   </div>
   <div id="theflashgohere">
   </div>
</asp:Content>
