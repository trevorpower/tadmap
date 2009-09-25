function setupMap(mapControlId, mapKey, iZoomLevels, iTileSize, getTileUrl) {
   try {
      if (GBrowserIsCompatible()) {
         //alert(iTileSize);
         var oMap = new GMap2(document.getElementById(mapControlId));
         oMap.setCenter(new GLatLng(0, 0), 1);
         oMap.addControl(new GLargeMapControl());

         var copyCollection = new GCopyrightCollection("Tadmap");
         var copyright = new GCopyright(1, new GLatLngBounds(new GLatLng(0, 0), new GLatLng(0.4, 0.2)), 0, "");
         copyCollection.addCopyright(copyright);

         function CustomTileCheckRange(tile, zoom, tileSize) {
            if (tile.x < 0)
               return false;

            if (tile.y < 0)
               return false;

            var size = Math.pow(2, zoom);

            if (tile.x >= size)
               return false;

            if (tile.y >= size)
               return false;

            return true;
         }
         
         function CustomIsPng() {
            return true;
         }

         var tilelayers = [new GTileLayer(copyCollection, 0, iZoomLevels)];
         tilelayers[0].getTileUrl = getTileUrl;
         tilelayers[0].isPng = CustomIsPng;


         var oMapOptions = {
            errorMessage: "Outside the bounds of the map.",
            tileSize: iTileSize
         }

         var oProjection = new GMercatorProjection(iZoomLevels + 1);
         oProjection.tileCheckRange = CustomTileCheckRange;

         oProjection.fromLatLngToPixel = function(latlng, z) {
            return new GPoint(latlng., 0);
         }

         oProjection.fromPixelToLatLng = function(latlng, z) {
            return new GLatLng(0, 0);
         }

         oProjection.getWrapWidth = function(z) {
            return Math.pow(2, z) * iTileSize;
         }

        

         var custommap = new GMapType(tilelayers, oProjection, "Tadmap", oMapOptions);
         oMap.addMapType(custommap);
         oMap.setMapType(custommap);

         var overviewControl = new GOverviewMapControl(new GSize(180, 160));
         overviewControl.setMapType(custommap);
         oMap.addControl(overviewControl);
         
      }
   }
   catch (e) {
      alert(e.message);
   }
 }
