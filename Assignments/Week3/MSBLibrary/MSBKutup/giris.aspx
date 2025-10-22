<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="MSBKutup.giris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="css/giris.css">

</head>
<body>

     <section class="container">
   <div class="login">
     <h1>Üye Giriş</h1>

  <form id="form1" runat="server">
      <div>
      
             <p>

                 <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

             </p>

             <p>
                 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
             </p>




             <p class="submit">

                 <asp:Button ID="Button1" runat="server" Text="Giriş" OnClick="Button1_Click" />

                 <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

             </p>
      
   </div>
          </div>
</form>
   <div class="login-help">
     <p> <a href="index.html">Şifremi Unuttum?</a>.</p>
   </div>
 </section>
  
</body>
</html>
