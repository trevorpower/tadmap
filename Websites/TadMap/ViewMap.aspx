<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewMap.aspx.cs"
   Inherits="ViewMap" Title="Tadmap" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
   Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script type="text/javascript" src="JavaScript/jquery-1.2.6.min.js"></script>

   <script type="text/javascript" src="JavaScript/jquery.jeditable.mini.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
   <asp:Label runat="server" ID="m_lblTitle" CssClass="EditTitle ItemTitle"></asp:Label>
   <div runat="server" id="panelImage" class="ImagePanel">
      <asp:Image runat="server" ID="m_imgPicture" CssClass="ItemDetailImage" BorderWidth="2px" />
      <div class="ImageButtons">
         <asp:HyperLink runat="server" ID="DownloadOriginal">Original</asp:HyperLink>
         <asp:LinkButton runat="server" ID="m_lbCreateTileSet" OnClick="m_lbCreateTileSet_Click">Create Tileset</asp:LinkButton>
         <asp:LinkButton runat="server" ID="m_lbViewTileSet">View Tileset</asp:LinkButton>
      </div>
   </div>
   <div class="ItemDetail">
      <asp:Label runat="server" ID="m_lblDescription" CssClass="EditDescription MapDescriptionText"></asp:Label>
   </div>

   <script language="javascript" type="text/javascript">
       $(document).ready(function() {
           $(".EditTitle").editable(function(value, settings){ UpdateImage.UpdateTitle(imageId, value); return value; }, {
               cssclass  : "ItemTitleEdit",
               indicator : "Saving...",
               tooltip   : "Click to edit...",
               cancel    : "Cancel",
               submit    : "Save"
           });
           $(".EditDescription").editable(function(value, settings){ UpdateImage.UpdateDescription(imageId, value); return value; }, {
               cssclass  : "MapDescriptionEdit",
               type      : "textarea",
               cancel    : "Cancel",
               submit    : "Save",
               indicator : "Saving...",
               tooltip   : "Click to edit..."
           });
       });
   </script>

</asp:Content>
