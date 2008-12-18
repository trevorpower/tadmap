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
   $("#PrivacyStatus").html("<b>Anyone</b> can view this image.");
}

function MadePrivate() {
   $("#PrivacyStatus").html("<b>Only you</b> can view this image.");
}

$(document).ready(function() {
   $(".EditTitle").editable(
      function(value, settings) {
         $.getJSON("/ImageAction/UpdateTitle/" + imageId + "/?title=" + value, function(json) {
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
   $(".EditDescription").editable(function(value, settings) {
      $.getJSON("/ImageAction/UpdateDescription/" + imageId + "/?description=" + value, function(json) {
      }); return value;
   }, {
      cssclass: "MapDescriptionEdit",
      type: "textarea",
      cancel: "Cancel",
      submit: "Save",
      indicator: "Saving...",
      tooltip: "Click to edit..."
   });
   $("#PublicCheckBox").checkBoxClick(
      function() {
         $.getJSON("/ImageAction/MakePublic/" + imageId, function(json) {
            MadePublic();
         })
      },
      function() {
         $.getJSON("/ImageAction/MakePrivate/" + imageId, function(json) {
             MadePrivate();
         })
      }
   );
});