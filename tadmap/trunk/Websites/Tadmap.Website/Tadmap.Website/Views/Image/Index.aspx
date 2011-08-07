<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" AutoEventWireup="true"
   CodeBehind="Index.aspx.cs" Inherits="Tadmap.Views.Image.Index" Title="Tadmap"
   Theme="Tad" %>

<asp:Content ContentPlaceHolderID="head" ID="HeadContent" runat="server">

   <script src="../../Scripts/jquery-1.2.6.min.js" type="text/javascript"></script>

   <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAym_kDX-_TVbmiGV7BeBJWxRC-CmrqIKGMGSZRD_eEh4qmhJZKBRHmfQ_rcFa-XUrvWIL-KK3v6EoZg"
      type="text/javascript"></script>

   <script src="../../Scripts/MapExplorer.js" type="text/javascript"></script>

   <% if (ViewData.Model.PreviewUrl != null)
      { %>

   <script type="text/javascript">
      //<![CDATA[

      var urls;

      function getTileUrl(tile, zoom) {
         return urls[zoom][Math.pow(2, zoom) * tile.x + tile.y];
      }
      
      
      function getTileUrls() {
      
         var result = $.ajax({
            url: '<%= Url.RouteUrl("Image", new { action = "GetTiles", id = ViewData.Model.Id }) %>',
            dataType: 'json',
            success: function()   { },
            data: {},
            async: false
         });

         return eval('(' + result.responseText + ')');
      }
      
      $(document).ready(function() {
         urls = getTileUrls();
         setupMap("map_canvas", '<%= ViewData.Model.StorageKey %>', <%= ViewData.Model.ZoomLevels %>, <%= ViewData.Model.TileSize %>, getTileUrl);
      })
      //]]>
   </script>

   <%}%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <% if (ViewData.Model.IsEditable)
      { %>

   <script src="../../Scripts/jquery.jeditable.mini.js" type="text/javascript"></script>

   <script language="javascript" type="text/javascript">
      $(document).ready(function() {
         $("#EditTitle").editable(
            function(value, settings) {
               $.getJSON('<%= Url.RouteUrl("Image", new { action = "UpdateTitle", id = ViewData.Model.Id }) %>' + '?title=' + value, function(json) {
               });
               return value;
            },
            {
               cssclass: "ItemTitleEdit",
               indicator: "Saving...",
               tooltip: "Click to edit...",
               cancel: "Cancel",
               submit: "Save"
            }
         );
         $("#EditDescription").editable(function(value, settings) {
            $.getJSON('<%= Url.RouteUrl("Image", new { action = "UpdateDescription", id = ViewData.Model.Id }) %>' + '?description=' + value, function(json) {
            }); return value;
         }, {
            cssclass: "MapDescriptionEdit",
            type: "textarea",
            cancel: "Cancel",
            submit: "Save",
            indicator: "Saving...",
            tooltip: "Click to edit..."
         });
      });
   </script>

   <% } %>
   <span id="EditTitle" class="ItemTitle"><%= ViewData.Model.Title %></span>
   <div class="ImagePanel">
      <% if (ViewData.Model.PreviewUrl != null)
         { %>
      <div id="map_canvas" class="ItemDetailImage" style="width: 700px; height: 600px">
      </div>
     <%-- <img class="ItemDetailImage" src="<%= System.Web.HttpUtility.HtmlAttributeEncode(ViewData.Model.PreviewUrl.OriginalString) %>"
         alt="<%= ViewData.Model.Title %>" />--%>
      <%}
         else
         { %>
      <div>
         This image is being processed. This may take several minutes.</div>
      <%} %>
      <div class="ImageButtons">
         <% if (ViewData.Model.OriginalUrl != null)
            { %>
         <a href="<%= ViewData.Model.OriginalUrl %>">Original</a>
         <%} %>
      </div>
   </div>
   <div class="ItemDetail">
      <span id="EditDescription" class="MapDescriptionText"><%= ViewData.Model.Description %></span>
   </div>
   <%
      if (ViewData.Model.IsEditable)
      {
         Html.RenderPartial("PrivacyControl", ViewData.Model);
      }
      if (ViewData.Model.ShowOffensiveCount)
      {
         Html.RenderPartial("OffensiveControl", ViewData.Model);
      }
   %>
</asp:Content>
