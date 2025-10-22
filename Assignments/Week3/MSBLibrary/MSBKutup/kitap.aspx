<%@ Page Title="" Language="C#" MasterPageFile="~/anasayfa.Master" AutoEventWireup="true" CodeBehind="kitap.aspx.cs" Inherits="MSBKutup.kitap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table> 
        <tr> <td><b>ID </b> </td> <td> <b>kitap adı</b></td> <td><b>detay     </b>     </td></tr>

        <asp:Repeater ID="Repeater1" runat="server">

            <ItemTemplate>  

       
        <tr> <td><%# Eval("ID") %></td> <td> <%# Eval("kitap") %></td> <td><a href=kitapdetay.aspx?id="<%# Eval("ID") %>" > detay</a></a></td></tr>
                </ItemTemplate>
         </asp:Repeater>
    </table>


</asp:Content>
