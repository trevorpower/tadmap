<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="MyImages.aspx.cs"
   Inherits="_Default" Title="Tadmap - My Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
   <div runat="server" id="divMapList" style="padding: 10px 30px 10px 30px;">
      <div class="NoteBox">
         <p>You are browsing all <b>your</b> images.</p>
         <p><asp:HyperLink ID="btnAddMap" runat="server" NavigateUrl="~/AddMap.aspx">Upload Image</asp:HyperLink></p>
      </div>
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
