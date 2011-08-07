function AddSyncPoint(oMap, oLatLong)
{
   var oIcon = new GIcon();
   oIcon.iconSize = new GSize(32, 32);
   oIcon.iconAnchor = new GPoint(16, 16);
   oIcon.infoWindowAnchor = new GPoint(25, 7);

   var oMarkerOptions = { 
     "icon": G_DEFAULT_ICON,
     "draggable": true, // not working with this version of labled marker
     "clickable": false,
     "labelText": "A",
     "labelOffset": new GSize(26, -20)
   };

   var oMarker = new LabeledMarker(oLatLong, oMarkerOptions);

   GEvent.addListener(oMarker, "dragstart", function() {
    oMap.closeInfoWindow();
   });

   GEvent.addListener(oMarker, "dragend", function() {
    //marker.openInfoWindowHtml("Here...");
   });

   oMap.addOverlay(oMarker);
}