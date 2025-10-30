<%@ Page Title="" Language="C#" MasterPageFile="~/anasayfa.Master" AutoEventWireup="true" CodeBehind="kitaplar.aspx.cs" Inherits="WEB.kitaplar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/kitap.css">
    <meta charset="utf-8" />
    <title>Kitaplar</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="Medeniyet Üniversitesi Kütüphane Sistemi - Kitaplar" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 20px;">
        <p>Kitaplar sayfasına hoş geldiniz!</p>
        <table class="book-table">
            <tr class="baslik">
                <td>ID</td>
                <td>Kitap Adı</td>
                <td>Yazar</td>
            </tr>
            <asp:Repeater ID="RepeaterKitaplar" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ID") %></td>
                        <td><%# Eval("kitap_adi") %></td>
                        <td><%# Eval("yazar") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="margin-top: 16px;">
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>


