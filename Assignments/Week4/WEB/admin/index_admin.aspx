<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="index_admin.aspx.cs" Inherits="WEB.admin.index_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 40px; text-align: center;">
        <h1 style="color: #615dee; font-size: 48px; margin-bottom: 20px;">Yönetici Paneli</h1>
        <p style="font-size: 24px; color: #333; margin-top: 30px;">
            Hoş geldiniz, <asp:Label ID="lblKullaniciAdi" runat="server" Text="Yönetici"></asp:Label>
        </p>
        <p style="font-size: 18px; color: #666; margin-top: 20px;">
            Kitapları yönetmek için menüden "Kitaplar Yönetimi" sekmesini kullanabilirsiniz.
        </p>
    </div>
</asp:Content>

