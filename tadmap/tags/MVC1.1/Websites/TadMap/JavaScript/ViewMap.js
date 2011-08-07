$.fn.checkBoxClick = function(onChecked, onUnchecked) {
   return this.each(function() {
      this.onclick = function() {
         if (this.checked)
            onChecked();
         else
            onUnchecked();
      };
   });
};

function MadePublic() {
   $(".PrivacyStatus").html("<b>Anyone</b> can view this image.");
}

function MadePrivate() {
   $(".PrivacyStatus").html("<b>Only you</b> can view this image.");
}

$(document).ready(function() {
   $(".EditTitle").editable(function(value, settings) { UpdateImage.UpdateTitle(imageId, value); return value; }, {
      cssclass: "ItemTitleEdit",
      indicator: "Saving...",
      tooltip: "Click to edit...",
      cancel: "Cancel",
      submit: "Save"
   });
   $(".EditDescription").editable(function(value, settings) { UpdateImage.UpdateDescription(imageId, value); return value; }, {
      cssclass: "MapDescriptionEdit",
      type: "textarea",
      cancel: "Cancel",
      submit: "Save",
      indicator: "Saving...",
      tooltip: "Click to edit..."
   });
   $("#ctl00_main_privacyCheckBox").checkBoxClick(
      function() {
         PageMethods.MakePublic(imageId, MadePublic);
      },
      function() {
         PageMethods.MakePrivate(imageId, MadePrivate);
      }
    );
});