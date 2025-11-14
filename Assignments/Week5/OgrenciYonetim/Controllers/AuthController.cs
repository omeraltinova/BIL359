using System;
using System.Web;
using System.Web.Mvc;
using OgrenciYonetim.Models;

namespace OgrenciYonetim.Controllers
{
    /// <summary>
    /// Kimlik doğrulama controller'ı (Login/Register)
    /// </summary>
    public class AuthController : Controller
    {
        private readonly KullaniciDB db = new KullaniciDB();

        /// <summary>
        /// Giriş sayfası
        /// GET: /Auth/Login
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Giriş işlemi
        /// POST: /Auth/Login
        /// </summary>
        [HttpPost]
        public ActionResult Login(string kulad, string sifre)
        {
            try
            {
                if (string.IsNullOrEmpty(kulad) || string.IsNullOrEmpty(sifre))
                {
                    ViewBag.Hata = "Kullanıcı adı ve şifre boş olamaz!";
                    return View();
                }

                Kullanici kullanici = db.Login(kulad, sifre);

                if (kullanici != null)
                {
                    // Session oluştur
                    Session.Timeout = 30;
                    Session["kullanici_id"] = kullanici.ID;
                    Session["kullanici_adi"] = kullanici.kulad;
                    Session["yetki"] = kullanici.yetki;

                    // Admin ise admin paneline, değilse ana sayfaya yönlendir
                    if (kullanici.yetki == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Kayıt işlemi
        /// POST: /Auth/Register
        /// </summary>
        [HttpPost]
        public ActionResult Register(string yeniKulad, string yeniSifre)
        {
            try
            {
                if (string.IsNullOrEmpty(yeniKulad) || string.IsNullOrEmpty(yeniSifre))
                {
                    ViewBag.KayitHata = "Kullanıcı adı ve şifre boş olamaz!";
                    return View("Login");
                }

                bool sonuc = db.Register(yeniKulad, yeniSifre);

                if (sonuc)
                {
                    ViewBag.KayitBasari = "Kayıt başarıyla oluşturuldu. Giriş yapabilirsiniz.";
                }
                else
                {
                    ViewBag.KayitHata = "Kayıt oluşturulamadı.";
                }

                return View("Login");
            }
            catch (Exception ex)
            {
                ViewBag.KayitHata = ex.Message;
                return View("Login");
            }
        }

        /// <summary>
        /// Çıkış işlemi
        /// GET: /Auth/Logout
        /// </summary>
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}

