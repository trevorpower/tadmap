<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
   Inherits="_Default" Title="Tadmap - Image hosting for map collectors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
   <div style="margin-left: auto; margin-right: auto; width: 540px; padding-top: 40px;
      padding-bottom: 40px;">
      <div style="text-align: center;">
         <div class="Instructions">
            Upload and browse through images of your favourite maps.</div>
      </div>
   </div>
   <div runat="server" id="divMapList" style="padding: 10px 30px 10px 30px;">
      <div style="border: none; margin: 5px; float: left; padding: 5px; height: 80px; width: 300px;
         background: #EEFFEE;">
         <asp:HyperLink ID="btnAddMap" runat="server" NavigateUrl="~/AddMap.aspx">Add Map</asp:HyperLink>
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
