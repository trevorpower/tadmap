<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="DotNetOpenId" TagPrefix="openid" Namespace="DotNetOpenId.Provider" %>
<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
   <title>Home Page</title>
</asp:Content>
<script runat="server">
</script>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
   <form runat="server">
   <pre>
      OpenIDStub
      {
         <openid:OpenIdProvider runat="server" ID="identity" OnNormalizeUri="Test" ProviderLocalIdentifier=  />
         <openid:ProviderEndpoint runat='server' ID="endpoint" OnAuthenticationChallenge ="Test"  />
      }
   </pre>
   </form>
</asp:Content>
