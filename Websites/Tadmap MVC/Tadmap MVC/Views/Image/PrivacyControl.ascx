<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrivacyControl.ascx.cs"
   Inherits="Tadmap_MVC.Views.Image.PrivacyControl" %>

<script language="javascript" type="text/javascript">
   function PrivacyChanged(isPublic) {
      $(".PrivacyCheckBox").attr("checked", isPublic);
      if (isPublic)
         $(".PrivacyStatus").html("<%= IsPublicDescription %>");
      else
         $(".PrivacyStatus").html("<%= IsPrivateDescription %>");
   }
   function SetPrivacy(imageId, isPublic) {
      if (isPublic)
         $.getJSON("/Image/" + imageId + "/MakePublic", PrivacyChanged);
      else
         $.getJSON("/Image/" + imageId + "/MakePrivate", PrivacyChanged);
   } 
</script>

<%= Html.CheckBox(
      "PublicCheckBox",
      ViewData.Model.IsPublic,
            new { id = "PublicCheckBox", onClick = "SetPrivacy('" + ViewData.Model.Id + "', this.checked);" })
%><label for="PublicCheckBox">Public</label>
<div style="font-size: 12px;">
   <span id="PrivacyStatus">
      <%= ViewData.Model.IsPublic ? IsPublicDescription : IsPrivateDescription%>
   </span>
</div>
