<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs"
   Inherits="Login" Title="tadmap - Sign In" %>

<%@ Register Assembly="DotNetOpenId" Namespace="DotNetOpenId.RelyingParty" TagPrefix="RP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script type="text/javascript" id="__openidselector" src="https://www.idselector.com/selector/09e5b013996c710abeb9547fccb8b638a5699b13"
      charset="utf-8"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
   <div style="margin-left: auto; margin-right: auto; width: 540px; padding-top: 40px;
      padding-bottom: 40px;">
      <div style="text-align: center;">
         <div class="Instructions">
            You can sign into <i>tadmap.com</i> by using any <a href="http://openid.net/what/">
               OpenID</a>.<br />
         </div>
         <div class="Instructions">
            Don't have an OpenID? Find out how to <a href="http://openid.net/get/">get one</a>.<br />
            <br />
         </div>
         <asp:Image ID="Image1" runat="server" Style="margin-left: auto; margin-right: auto;"
            ImageUrl="~/App_Themes/Tad/Login/openid-logo-small.png" />
      </div>
      <br />
      <div style="border: solid 0px red; vertical-align: top;">
         <input type="text" id="openid_url" name="openid_url" class="OpenIdUrlBox" size="40" />
         <asp:LinkButton ID="btnLogin" Class="LoginInButton" runat="server" OnClick="btnLogin_Click">Sign In</asp:LinkButton>
      </div>
      <asp:Label ID="lblError" runat="server" CssClass="ErrorMessage"></asp:Label>
   </div>
</asp:Content>
