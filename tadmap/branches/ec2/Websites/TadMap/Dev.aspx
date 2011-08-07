<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Dev.aspx.cs" Inherits="Dev" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAym_kDX-_TVbmiGV7BeBJWxRC-CmrqIKGMGSZRD_eEh4qmhJZKBRHmfQ_rcFa-XUrvWIL-KK3v6EoZg"
      type="text/javascript"></script>

   <script src="http://gmaps-utility-library.googlecode.com/svn/trunk/labeledmarker/release/src/labeledmarker_packed.js"
      type="text/javascript"></script>

    <script type="text/javascript">

    //<![CDATA[

    function loadGmap() {
      if (GBrowserIsCompatible()) {
        var map = new GMap2(document.getElementById("gmap"));
        var center = new GLatLng(37.4419, -122.1419);
        map.setCenter(center, 3);
        map.addControl(new GSmallMapControl());
        
        GEvent.addListener(map, "click", function (overlay, latlng) {
           if (overlay == null)
           {
               AddSyncPoint(map, latlng);
           }
        });        
      }
    }

    //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
   <div id="tmap" style="width: 550px; height: 350px"></div>
   <hr />
   <div id="gmap" style="width: 550px; height: 350px"></div>
</asp:Content>

