<%@ Page Title="Administrator Functions" Language="C#" MasterPageFile="~/Views/Shared/Main.master"
   Inherits="System.Web.Mvc.ViewPage" Theme="Tad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <script src="../../Scripts/jquery-1.2.6.min.js" type="text/javascript"></script>
   
   <script language="javascript" type="text/javascript">
      function Done() {
         alert('Done!');
      }
   </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <a onclick='$.getJSON("<%= Url.RouteUrl(new { controller = "Admin", action = "Works" }) %>", Done);'>
      getJSON to an action that works </a>
   <br />
   <a onclick='$.getJSON("<%= Url.RouteUrl(new { controller = "Admin", action = "ThrowsException" }) %>", Done);'>
      getJSON to an action that throws an exception </a>
   <br />
   <a onclick='$.getJSON("<%= Url.RouteUrl(new { controller = "Admin", action = "NotAuthorized" }) %>", Done);'>
      getJSON to an action that I don'h have authorization to use </a>
   <hr />
   <% Html.RenderPartial("ImageListControl", ViewData.Model); %>
</asp:Content>
