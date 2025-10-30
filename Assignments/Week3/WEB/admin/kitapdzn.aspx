<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="kitapdzn.aspx.cs" Inherits="WEB.admin.kitapdzn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <title>Kitap Düzenle</title>
    <link rel="stylesheet" href="../css/kitap.css">
    <style>
        .form-field{margin:10px 0}
        .form-field label{display:block;margin-bottom:6px;color:#333}
        .form-field input{padding:8px;width:320px}
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:20px;">
        <h2 style="color:#615dee;">Kitap <asp:Literal ID="litBaslik" runat="server" /></h2>
        <div class="form-field">
            <label for="txtKitapAdi">Kitap Adı</label>
            <asp:TextBox ID="txtKitapAdi" runat="server" />
        </div>
        <div class="form-field">
            <label for="txtYazar">Yazar</label>
            <asp:TextBox ID="txtYazar" runat="server" />
        </div>
        <div class="form-field">
            <label for="txtIsbn">ISBN</label>
            <asp:TextBox ID="txtIsbn" runat="server" />
        </div>
        <div class="form-field">
            <label for="txtYil">Yıl</label>
            <asp:TextBox ID="txtYil" runat="server" />
        </div>
        <div style="margin-top:16px;">
            <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn" OnClick="btnKaydet_Click" />
            <a href="kitaplar.aspx" style="margin-left:12px;">Geri</a>
        </div>
        <div style="margin-top:16px;">
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
        </div>
    </div>
    </asp:Content>


