<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="uyeler.aspx.cs" Inherits="WEB.admin.uyeler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <title>Üyeler</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 20px;">
        <h2 style="color:#615dee;">Üyeler</h2>
        <div style="margin:10px 0;">
            <a href="uyedzn.aspx">+ Yeni Üye</a>
        </div>
        <table class="book-table">
            <tr class="baslik">
                <td>ID</td>
                <td>Kullanıcı Adı</td>
                <td>Yetki</td>
                <td>Durum</td>
                <td>İşlemler</td>
            </tr>
            <asp:Repeater ID="RepeaterUyeler" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ID") %></td>
                        <td><%# Eval("kulad") %></td>
                        <td><%# Eval("yetki") %></td>
                        <td><%# Eval("durum") %></td>
                        <td>
                            <a href='uyedzn.aspx?id=<%# Eval("ID") %>'>Düzenle</a> | 
                            <a href='uyesil.aspx?id=<%# Eval("ID") %>'>Sil</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="margin-top:16px;">
            <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>


