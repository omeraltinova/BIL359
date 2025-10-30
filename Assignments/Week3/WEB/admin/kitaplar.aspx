<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="kitaplar.aspx.cs" Inherits="WEB.admin.kitaplar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/kitap.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 20px;">
        <h2 style="color: #615dee; font-size: 32px; margin-bottom: 20px;">Kitaplar Yönetimi</h2>
        <div style="margin-bottom: 12px;">
            <a href="kitapdzn.aspx">+ Yeni Kitap Ekle</a>
        </div>
        
        <table class="book-table">
            <tr class="baslik">
                <td>ID</td>
                <td>Kitap Adı</td>
                <td>Yazar</td>
                <td>İşlemler</td>
            </tr>

            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ID") %></td>
                        <td><%# Eval("kitap_adi") %></td>
                        <td><%# Eval("yazar") %></td>
                        <td>
                            <a href='kitapdzn.aspx?id=<%# Eval("ID") %>'>Düzenle</a> | 
                            <a href='kitapsil.aspx?id=<%# Eval("ID") %>'>Sil</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div style="margin-top: 20px;">
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>

