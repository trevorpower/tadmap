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
});