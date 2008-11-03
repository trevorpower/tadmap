function TadTileLayer()
{
   var oCopyright = new GCopyright(
      1,
      new GLatLngBounds(new GLatLng(-90, -180), new GLatLng(90, 180)),
      0,
      "©2008 tadmap.com"
   );
   
   var oCopyrightCollection = new GCopyrightCollection('Chart');
   oCopyrightCollection.addCopyright(oCopyright);
   
   GTileLayer.call(this, oCopyrightCollection, 0, 13);
}

TadTileLayer.prototype = new GTileLayer();

TadTileLayer.prototype.getTileUrl = function(tile, zoom)
{
   return 'http://tadmap-dev.s3.amazonaws.com/c17d087f-0799-48e1-a545-9ebdab93b55c.gif';
}

TadTileLayer.prototype.isPng = function()
{
   alert('isPng');
   return false;
}

TadTileLayer.prototype.getOpacity = function()
{
   alert('get opacity');
   return 1.0;
}

TadTileLayer.prototype.getCopyright = function(bounds, zoom)
{
   alert('get copyright');
   return "tadmap.com";
}

function setupMap(mapControlId, mapKey, iZoomLevels, iTileSize)
 {
   if (GBrowserIsCompatible())
   {
      var oMap = new GMap2(document.getElementById(mapControlId));
      oMap.setCenter(new GLatLng(0, 0), 1);
      oMap.addControl(new GSmallMapControl());
     
      var copyCollection = new GCopyrightCollection("tadmap.com");
      var copyright = new GCopyright(1, new GLatLngBounds(new GLatLng(0, 0), new GLatLng(0.2, 0.1)), 0, "");
      copyCollection.addCopyright(copyright);

      var tilelayers = [new GTileLayer(copyCollection, 0, iZoomLevels)];
      tilelayers[0].getTileUrl = CustomGetTileUrl;
      tilelayers[0].isPng = CustomIsPng;
         
      function CustomGetTileUrl(location,zoom) {
         return 'http://tadmap-dev.s3.amazonaws.com/Tile_' + location.x + '_' + location.y + '_' + zoom + '_' + mapKey;
      }

      function CustomIsPng() {
         return true;
      }
      
      var oMapOptions = {
         errorMessage:"Outside the bounds of the map.",
         tileSize: iTileSize
      }
      
      var oProjection =  new GMercatorProjection(iZoomLevels + 1);
      oProjection.tileCheckRange = CustomTileCheckRange;
      
      function CustomTileCheckRange(tile, zoom, tileSize)
      {
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
      
      var custommap = new GMapType(tilelayers, oProjection, "tadmap", oMapOptions);
      oMap.addMapType(custommap);
      oMap.setMapType(custommap);
      
     GEvent.addListener(map, "click", function (overlay, latlng) {
        if (overlay == null)
        {
            AddSyncPoint(oMap, latlng);
        }
     });        

   }
 }
