<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="WEB.giris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Giriş - İMÜ Kütüphanesi</title>
    <link rel="shortcut icon" type="image/png" href="images/logo.png">
    <link rel="stylesheet" href="css/genel.css">
</head>
<body class="body">
    <div id="login-container">
        <div id="auth-grid">
            <div id="login-card">
                <h1 id="auth-text">Giriş</h1>
                <form id="form1" runat="server">
                    <div class="field">
                        <label for="txtKullaniciAdi">Kullanıcı Adı</label>
                        <asp:TextBox ID="txtKullaniciAdi" runat="server" placeholder="Kullanıcı adınız"></asp:TextBox>
                    </div>

                    <div class="field">
                        <label for="txtSifre">Şifre</label>
                        <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" placeholder="*******"></asp:TextBox>
                    </div>

                    <div class="auth-button">
                        <asp:Button ID="btnGiris" runat="server" Text="Giriş Yap" CssClass="btn" OnClick="btnGiris_Click" />
                    </div>

                    <div style="margin-top: 15px;">
                        <asp:Label ID="lblMesaj" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </form>
            </div>

            <div id="register-card">
                <h2 id="auth-text">Bilgi</h2>
                <p style="color: #000; font-size: 18px; margin-top: 20px;">
                    İMÜ Kütüphane Sistemine hoş geldiniz.<br /><br />
                    Giriş yapmak için kullanıcı adı ve şifrenizi giriniz.
                </p>
            </div>
        </div>
    </div>
</body>
</html>

