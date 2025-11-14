using System;
using System.Web.Mvc;
using OgrenciYonetim.Models;

namespace OgrenciYonetim.Controllers
{
    /// <summary>
    /// Admin paneli controller'ı
    /// </summary>
    public class AdminController : Controller
    {
        private readonly KullaniciDB kullaniciDb = new KullaniciDB();

        /// <summary>
        /// Admin kontrolü
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Admin kontrolü
            if (Session["yetki"] == null || Session["yetki"].ToString() != "admin")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Auth", action = "Login" }
                    )
                );
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Admin ana sayfası
        /// GET: /Admin/Index
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.KullaniciAdi = Session["kullanici_adi"];
            return View();
        }

        /// <summary>
        /// Kullanıcı yönetimi
        /// GET: /Admin/Kullanicilar
        /// </summary>
        public ActionResult Kullanicilar()
        {
            try
            {
                var kullanicilar = kullaniciDb.GetAll();
                return View(kullanicilar);
            }
            catch (Exception ex)
            {
                ViewBag.Hata = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Kullanıcı silme
        /// POST: /Admin/KullaniciSil
        /// </summary>
        [HttpPost]
        public ActionResult KullaniciSil(int id)
        {
            try
            {
                bool sonuc = kullaniciDb.Delete(id);
                
                if (sonuc)
                {
                    TempData["Basari"] = "Kullanıcı başarıyla silindi!";
                }
                else
                {
                    TempData["Hata"] = "Kullanıcı silinirken bir hata oluştu.";
                }

                return RedirectToAction("Kullanicilar");
            }
            catch (Exception ex)
            {
                TempData["Hata"] = ex.Message;
                return RedirectToAction("Kullanicilar");
            }
        }
    }
}

