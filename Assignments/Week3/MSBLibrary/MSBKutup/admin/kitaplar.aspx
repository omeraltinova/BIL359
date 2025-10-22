<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="kitaplar.aspx.cs" Inherits="MSBKutup.admin.kitaplar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table> 
       <tr> <td><b>ID </b> </td> <td> <b>kitap adı</b></td> <td><b>İşlemler     </b>     </td></tr>

       <asp:Repeater ID="Repeater1" runat="server">

           <ItemTemplate>  

      
       <tr> 
           <td><%# Eval("ID") %></td> 
           <td> <%# Eval("kitap") %></td>
           <td><a href=kitapsil.aspx?id="<%# Eval("ID") %>" > Sil</a>
           
           <a href=kitapdzn.aspx?id="<%# Eval("ID") %>" > Düzenle</a></td>


       </tr>
               </ItemTemplate>
        </asp:Repeater>
   </table>
</asp:Content>
