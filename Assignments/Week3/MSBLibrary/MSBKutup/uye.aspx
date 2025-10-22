<%@ Page Title="" Language="C#" MasterPageFile="~/anasayfa.Master" AutoEventWireup="true" CodeBehind="uye.aspx.cs" Inherits="MSBKutup.uye" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table>

<tr>   <td>         <strong>üye olunuz
</strong>
</td> </tr>

<tr>   <td>        kulalnıcı adı:</td>   <td>        
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </td </tr>

    <tr>   <td>        şifre</td>   <td>        
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td </tr>

    <tr>   <td>        &nbsp;</td>   <td>        
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="kaydet" />
        </td </tr>

    </table>



&nbsp;&nbsp;&nbsp; 



</asp:Content>
