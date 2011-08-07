<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AllImages.aspx.cs" Inherits="Admin_AllImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
    <div runat="server" id="divMapList" style="padding: 10px 30px 10px 30px;">
      <asp:Repeater ID="m_repMapRepeater" runat="server">
         <ItemTemplate>
            <div runat="server" id="ListItem" class="ImageListItem">
               <asp:Image runat="server" ID="m_imgImage" Style="float: left; margin-right: 5px;" />
               <div class="TextArea">
                  <asp:LinkButton runat="server" ID="m_lbName"></asp:LinkButton>
                  <asp:Label runat="server" CssClass="ImageListItemDescription" ID="m_lblDescription"></asp:Label>
               </div>
            </div>
         </ItemTemplate>
      </asp:Repeater>
   </div>
</asp:Content>

