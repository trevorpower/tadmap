<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="tadmap - Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
    <div style="margin-left: auto; margin-right: auto; width: 540px; padding-top: 40px;
      padding-bottom: 40px;">
      <div style="text-align: center;">
         <asp:Image ID="Image1" runat="server" Style="margin-left: auto; margin-right: auto;"
            ImageUrl="~/App_Themes/Tad/Login/openid-logo-small.png" />
      </div>
      <br />
      <div style="border: solid 1px red; vertical-align: top;">
            <input type="text" id="openid_url" name="openid_url" class="OpenIdUrlBox" size="40" />
            <asp:LinkButton ID="btnLogin" Class="LoginInButton" runat="server" 
               >Register</asp:LinkButton>
      </div>
   </div>
</asp:Content>

