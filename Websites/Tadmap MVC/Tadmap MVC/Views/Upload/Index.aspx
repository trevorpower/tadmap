<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs"
   Inherits="Tadmap_MVC.Views.Upload.Index" Title="Tadmap - Upload Image" Theme="Tad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div class="ItemDetailListEdit">
      <div class="ItemDetailEdit">
         <asp:Label ID="Label2" runat="server" CssClass="ItemDetailControlLabel">Title</asp:Label>
         <asp:TextBox ID="m_txtTitle" runat="server" CssClass="ItemDetailControl"></asp:TextBox>
      </div>
      <div class="ItemDetailEdit">
         <asp:Label ID="Label1" runat="server" CssClass="ItemDetailControlLabel">Description</asp:Label>
         <asp:TextBox ID="m_txtDescription" runat="server" CssClass="ItemDetailControl" MaxLength="1024"
            TextMode="MultiLine"></asp:TextBox>
      </div>
      <div class="ItemDetailEdit">
         <asp:Label ID="Label3" runat="server" CssClass="ItemDetailControlLabel">File</asp:Label>
         <input id="FileInput" runat="server" type="file" class="ItemDetailControl" />
      </div>
   </div>
   <div style="margin-left: auto; margin-right: auto; clear: both; text-align: center;
      padding: 12px;">
      <asp:LinkButton CssClass="Button" Style="width: 80px;" ID="m_btnAdd" runat="server" Text="Upload" OnClick="m_btnAdd_Click" />
   </div>
</asp:Content>
