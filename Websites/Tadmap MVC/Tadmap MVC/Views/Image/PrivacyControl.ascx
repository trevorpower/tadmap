<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrivacyControl.ascx.cs"
   Inherits="Tadmap_MVC.Views.Image.PrivacyControl" %>

<script language="javascript" type="text/javascript">
   function PrivacyChanged(isPublic) {
      $("#PrivacyStatus").css("color", "green");
      if (isPublic) {
         $("#PrivacyStatus").html("<%= IsPublicDescription %>");
         $("#PublicCheckBox").attr("checked", "checked");
      }
      else {
         $("#PrivacyStatus").html("<%= IsPrivateDescription %>");
         $("#PublicCheckBox").removeAttr("checked", "checked");
      }
   }
   function SetPrivacy(imageId, isPublic) {
      $("#PrivacyStatus").css("color", "orange");
      if (isPublic)
         $.getJSON("/Image/" + imageId + "/MakePublic", PrivacyChanged);
      else
         $.getJSON("/Image/" + imageId + "/MakePrivate", PrivacyChanged);
   } 
</script>

<%= Html.CheckBox(
      "PublicCheckBox",
      ViewData.Model.IsPublic,
            new { onClick = "SetPrivacy('" + ViewData.Model.Id + "', this.checked);" })
%><label for="PublicCheckBox">Public</label>
<div style="font-size: 12px;">
   <span id="PrivacyStatus">
      <%= ViewData.Model.IsPublic ? IsPublicDescription : IsPrivateDescription%>
   </span>
</div>
