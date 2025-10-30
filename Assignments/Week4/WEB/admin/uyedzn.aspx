<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="uyedzn.aspx.cs" Inherits="WEB.admin.uyedzn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <title>Üye Düzenle</title>
    <style>
        .form-field{margin:10px 0}
        .form-field label{display:block;margin-bottom:6px;color:#333}
        .form-field input, .form-field select{padding:8px;width:320px}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:20px;">
        <h2 style="color:#615dee;">Üye <asp:Literal ID="litBaslik" runat="server" /></h2>
        <div class="form-field">
            <label for="txtKulad">Kullanıcı Adı</label>
            <asp:TextBox ID="txtKulad" runat="server" />
        </div>
        <div class="form-field">
            <label for="txtSifre">Şifre <span style="color:#777;">(güncellemede boş bırakılırsa değişmez)</span></label>
            <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" />
        </div>
        <div class="form-field">
            <label for="ddlYetki">Yetki</label>
            <asp:DropDownList ID="ddlYetki" runat="server">
                <asp:ListItem Text="Üye" Value="uye" />
                <asp:ListItem Text="Admin" Value="admin" />
            </asp:DropDownList>
        </div>
        <div class="form-field">
            <label for="ddlDurum">Durum</label>
            <asp:DropDownList ID="ddlDurum" runat="server">
                <asp:ListItem Text="Aktif" Value="aktif" />
                <asp:ListItem Text="Pasif" Value="pasif" />
                <asp:ListItem Text="Engelli" Value="engelli" />
            </asp:DropDownList>
        </div>
        <div style="margin-top:16px;">
            <asp:Button ID="btnKaydet" runat="server" Text="Kaydet" CssClass="btn" OnClick="btnKaydet_Click" />
            <a href="uyeler.aspx" style="margin-left:12px;">Geri</a>
        </div>
        <div style="margin-top:16px;">
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>


