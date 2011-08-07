<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OffensiveControl.ascx.cs"
   Inherits="Tadmap_MVC.Views.Image.OffensiveControl" %>

<script language="javascript" type="text/javascript">
      function MadeOffensive(newCount) {
         $("#offesniveCount").text(newCount);
         $("#unmarkOffensive").show("slow");
      }
      function MadeUnoffensive(newCount) {
         $("#offesniveCount").text(newCount);
         $("#markOffensive").show("slow");
      }
      function MarkAsOffensive(imageId) {
         $("#markOffensive").hide("slow");
         $.getJSON("/Image/" + imageId + "/Mark", MadeOffensive);
      }
      function UnmarkAsOffensive(imageId) {
         $("#unmarkOffensive").hide("slow");
         $.getJSON("/Image/" + imageId + "/UnMark", MadeUnoffensive);
      } 
</script>

<div id="administrator">
   <span id="offensiveCount">
      <%= ViewData.Model.OffensiveCount %>
   </span><span id="markOffensive" style='display: <%= ViewData.Model.CanMarkOffensive ? "inline" : "none" %>;'
      onclick="MarkAsOffensive('<%= ViewData.Model.Id %>');">
      <%= Tadmap_MVC.Views.Image.ViewResources.MarkAsOffensive %>
   </span><span id="unmarkOffensive" style='display: <%= ViewData.Model.CanUnmarkOffensive ? "inline" : "none" %>;'
      onclick="UnmarkAsOffensive('<%= ViewData.Model.Id %>');">
      <%= Tadmap_MVC.Views.Image.ViewResources.MarkAsNotOffensive %></span>
</div>
